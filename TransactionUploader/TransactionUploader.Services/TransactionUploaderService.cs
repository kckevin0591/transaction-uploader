using System;
using System.IO;
using System.Threading.Tasks;
using TransactionUploader.Common;
using TransactionUploader.Common.Exceptions;
using TransactionUploader.Repository;

namespace TransactionUploader.Services
{
    public class TransactionUploaderService : ITransactionUploaderService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IExtractorManager _extractorManager;

        public TransactionUploaderService(
            ITransactionRepository transactionRepository,
            IExtractorManager extractorManager
            )
        {
            _transactionRepository = transactionRepository;
            _extractorManager = extractorManager;
        }

        public async Task Upload(string fileType, string data)
        {
            var transactions = _extractorManager.GetExtractor(fileType).Extract(data);   
            await _transactionRepository.Save(transactions).ConfigureAwait(false);   
        }

    }
}