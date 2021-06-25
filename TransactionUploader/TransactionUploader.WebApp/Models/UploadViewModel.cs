using Microsoft.AspNetCore.Http;

namespace TransactionUploader.WebApp.Models
{
    public class UploadViewModel
    {
        public IFormFile File { get; set; }
        public bool HasError { get; set; }
        public string ErrorMessage { get; set; }
        public bool IsSuccess { get; set; }
    }
}