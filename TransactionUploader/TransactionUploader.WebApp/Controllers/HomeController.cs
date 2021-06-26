using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionUploader.Common;
using TransactionUploader.Repository;
using TransactionUploader.WebApp.Models;

namespace TransactionUploader.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITransactionRepository _transactionRepository;

        public HomeController(ILogger<HomeController> logger,
        ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        
        public IActionResult Index()
        {
            return View(new UploadViewModel());
        }

        public IActionResult Search()
        {
            var filter = new TransactionFilterModel()
            {
                Status = "A",
                CurrencyCode = "USD",
                StartDate = new DateTime(2018,1,1,0,0,0,DateTimeKind.Local),
                EndDate = DateTime.Now
            };
            return View(filter);
        }

        public async Task<IActionResult> GetTransactions(TransactionFilterModel filter)
        {
            var txns = await _transactionRepository.GetByFilter(new TransactionFilter()
            {
                Status = filter.Status,
                CurrencyCode = filter.CurrencyCode,
                StartDate = filter.StartDate,
                EndDate = filter.EndDate
            });

            var model = new SearchTransactionViewModel
            {
                Transactions = txns.Select(t=> t.ConvertToApiTransactionModel()).ToList()
            };
            return ViewComponent("TransactionList", model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
