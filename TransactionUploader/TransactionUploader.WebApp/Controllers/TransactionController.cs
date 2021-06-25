using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionUploader.Common;
using TransactionUploader.Repository;
using TransactionUploader.Services;
using TransactionUploader.WebApp.Models;

namespace TransactionUploader.WebApp.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionUploaderService _transactionUploader;
        private readonly ITransactionRepository _transactionRepository;

        public TransactionController(ITransactionUploaderService transactionUploader,
            ITransactionRepository transactionRepository)
        {
            _transactionUploader = transactionUploader;
            _transactionRepository = transactionRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            
            if (file == null || file.Length == 0)
            {
                return BadRequest("File Not Found");
            }

            await using (var dataStream = new MemoryStream())
            {
                var fileType = Path.GetExtension(file.FileName).ToLower().Replace(".","");
                await file.CopyToAsync(dataStream);
                dataStream.Seek(0, SeekOrigin.Begin);
                using (var reader = new StreamReader(dataStream, Encoding.UTF8))
                {
                    var dataString = await reader.ReadToEndAsync().ConfigureAwait(false);
                    await _transactionUploader.Upload(fileType, dataString);
                }
            }
            
            return Ok();
        }

        [HttpGet("{currency}")]
        public async Task<IActionResult> ByCurrency(string currency)
        {
            var txns = await _transactionRepository.GetByCurrency(currency).ConfigureAwait(false);
            return new JsonResult(txns.Select(ConvertToApiTransactionModel));
        }

        [HttpGet("{status}")]
        public async Task<IActionResult> ByStatus(string status)
        {
            var txns = await _transactionRepository.GetByStatus(status).ConfigureAwait(false);
            return new JsonResult(txns.Select(ConvertToApiTransactionModel));
        }

        [HttpGet]
        public async Task<IActionResult> ByDate(DateTime startDate, DateTime endDate)
        {
            var txns = await _transactionRepository.GetDateRange(startDate, endDate).ConfigureAwait(false);
            return new JsonResult(txns.Select(ConvertToApiTransactionModel));
        }

        private static ApiTransactionModel ConvertToApiTransactionModel(Transaction txn)
        {
            return new ApiTransactionModel()
            {
                Id = txn.TransactionId,
                Status = txn.Status,
                Payment = $"{txn.Amount} {txn.CurrencyCode}"
            };
        }
    }
}
