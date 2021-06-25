using System;
using System.Threading.Tasks;

namespace TransactionUploader.Services
{
    public interface ITransactionUploaderService
    {
        Task Upload(string fileType, string data);
    }
}
