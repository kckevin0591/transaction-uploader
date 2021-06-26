using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TransactionUploader.Common;

namespace TransactionUploader.Repository
{
    public interface ITransactionRepository
    {
        Task Save(IEnumerable<Transaction> transactions);
        Task<IEnumerable<Transaction>> GetByCurrency(string currencyCode);
        Task<IEnumerable<Transaction>> GetByStatus(string status);
        Task<IEnumerable<Transaction>> GetDateRange(DateTime startDate, DateTime endDate);
        Task<IEnumerable<Transaction>> GetByFilter(TransactionFilter filter);
        Task<IEnumerable<Transaction>> GetAll();
    }
}