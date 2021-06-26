using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionUploader.Common;
using TransactionUploader.Common.Exceptions;
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
        [RequestSizeLimit(1048576)]
        public async Task<IActionResult> Upload(IFormFile file)
        {

            if (file == null || file.Length == 0)
            {
                return BadRequest(new { Message = "File Not Found" });
            }

            try
            {
                await using (var dataStream = new MemoryStream())
                {
                    var fileType = Path.GetExtension(file.FileName).ToLower().Replace(".", "");
                    await file.CopyToAsync(dataStream);
                    dataStream.Seek(0, SeekOrigin.Begin);
                    using (var reader = new StreamReader(dataStream, Encoding.UTF8))
                    {
                        var dataString = await reader.ReadToEndAsync().ConfigureAwait(false);
                        await _transactionUploader.Upload(fileType, dataString);
                    }
                }
            }
            catch (InvalidFileTypeException)
            {
                return BadRequest(new {Message = "Unknown format"});
            }
            catch (InvalidFileContentException e)
            {
                return BadRequest(new {e.Message, ApiErrors = e.Errors});
            }
            catch (StorageFailureException)
            {
                var ApiErrors = new List<string>() {"Possible duplicate transaction id"};
                return BadRequest(new { Message = "Failed to store file content", ApiErrors});
            }
            catch (Exception e)
            {
                var ApiErrors = new List<string>() {e.Message};
                return BadRequest(new { Message = "Unexpected Error", ApiErrors});
            }

            return Ok();
        }

        [HttpGet("{currency}")]
        public async Task<IActionResult> ByCurrency(string currency)
        {
            var txns = await _transactionRepository.GetByCurrency(currency).ConfigureAwait(false);
            return new JsonResult(txns.Select(t => t.ConvertToApiTransactionModel()));
        }

        [HttpGet("{status}")]
        public async Task<IActionResult> ByStatus(string status)
        {
            var txns = await _transactionRepository.GetByStatus(status).ConfigureAwait(false);
            return new JsonResult(txns.Select(t => t.ConvertToApiTransactionModel()));
        }

        [HttpGet]
        public async Task<IActionResult> ByDate(DateTime startDate, DateTime endDate)
        {
            var txns = await _transactionRepository.GetDateRange(startDate, endDate).ConfigureAwait(false);
            return new JsonResult(txns.Select(t => t.ConvertToApiTransactionModel()));
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var txns = await _transactionRepository.GetAll().ConfigureAwait(false);
            return new JsonResult(txns.Select(t => t.ConvertToApiTransactionModel()));
        }
    }
}
