using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TransactionUploader.Common;
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

        public async Task<IEnumerable<Transaction>> GetByCurrency(string currencyCode)
        {
            return await _context.Transactions.
                Where(t => t.CurrencyCode == currencyCode)
                .Select(a=> new Transaction()
                {
                    TransactionId = a.Id,
                    TransactionDate = a.TransactionDate,
                    Amount = Convert.ToDecimal(a.Amount),
                    CurrencyCode = a.CurrencyCode,
                    Status = a.Status
                }).ToListAsync();
        }

        public async Task<IEnumerable<Transaction>> GetByStatus(string status)
        {
            return await _context.Transactions.
                Where(t => t.Status == status)
                .Select(a=> new Transaction()
                {
                    TransactionId = a.Id,
                    TransactionDate = a.TransactionDate,
                    Amount = Convert.ToDecimal(a.Amount),
                    CurrencyCode = a.CurrencyCode,
                    Status = a.Status
                }).ToListAsync();
        }

        public async Task<IEnumerable<Transaction>> GetDateRange(DateTime startDate, DateTime endDate)
        {
            return await _context.Transactions.
                Where(t => t.TransactionDate >= startDate && t.TransactionDate <= endDate)
                .Select(a=> new Transaction()
                {
                    TransactionId = a.Id,
                    TransactionDate = a.TransactionDate,
                    Amount = Convert.ToDecimal(a.Amount),
                    CurrencyCode = a.CurrencyCode,
                    Status = a.Status
                }).ToListAsync();
        }

        public async Task<IEnumerable<Transaction>> GetByFilter(TransactionFilter filter)
        {
            return await _context.Transactions.
                Where(t => 
                    t.TransactionDate >= filter.StartDate && 
                    t.TransactionDate <= filter.EndDate &&
                    t.Status == filter.Status &&
                    t.CurrencyCode == filter.CurrencyCode)
                .Select(a=> new Transaction()
                {
                    TransactionId = a.Id,
                    TransactionDate = a.TransactionDate,
                    Amount = Convert.ToDecimal(a.Amount),
                    CurrencyCode = a.CurrencyCode,
                    Status = a.Status
                }).ToListAsync();
        }
    }
}
