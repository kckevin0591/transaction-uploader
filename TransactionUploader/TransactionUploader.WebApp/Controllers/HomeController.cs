using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionUploader.Services;
using TransactionUploader.WebApp.Models;

namespace TransactionUploader.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ITransactionUploaderService _transactionUploader;

        public HomeController(ILogger<HomeController> logger,
        ITransactionUploaderService transactionUploader)
        {
            _logger = logger;
            _transactionUploader = transactionUploader;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(new UploadViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Index(UploadViewModel model)
        {
            
            if (model.File == null || model.File.Length == 0)
            {
                return BadRequest("File Not Found");
            }

            await using (var dataStream = new MemoryStream())
            {
                var fileType = model.File.ContentType.ToLower().Split('/');
                await model.File.CopyToAsync(dataStream);
                dataStream.Seek(0, SeekOrigin.Begin);
                using (var reader = new StreamReader(dataStream, Encoding.UTF8))
                {
                    var dataString = await reader.ReadToEndAsync().ConfigureAwait(false);
                    await _transactionUploader.Upload(fileType[1], dataString);
                }
            }

            model.IsSuccess = true;
            return Ok();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
