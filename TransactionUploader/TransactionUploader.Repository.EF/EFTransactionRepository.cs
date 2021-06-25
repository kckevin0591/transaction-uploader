using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransactionUploader.Repository.EF.Models;
using Transaction = TransactionUploader.Common.Transaction;

namespace TransactionUploader.Repository.EF
{
    public class EFTransactionRepository:ITransactionRepository
    {
        private readonly test_dbContext _context;

        public EFTransactionRepository(test_dbContext context)
        {
            _context = context;
        }

        public async Task Save(IEnumerable<Transaction> transactions)
        {
            var dbTxns = transactions.Select(t => new TransactionUploader.Repository.EF.Models.Transaction()
            {
                Id = t.TransactionId,
                TransactionDate = t.TransactionDate,
                Amount = Convert.ToDouble(t.Amount),
                Status = t.Status,
                CurrencyCode = t.CurrencyCode
            });

            _context.Transactions.AddRange(dbTxns);
            await _context.SaveChangesAsync();
        }
    }
}
