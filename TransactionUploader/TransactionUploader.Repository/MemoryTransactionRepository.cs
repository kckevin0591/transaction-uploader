using System.Collections.Generic;
using System.Threading.Tasks;
using TransactionUploader.Common;

namespace TransactionUploader.Repository
{
    public class MemoryTransactionRepository:ITransactionRepository
    {
        private static readonly List<Transaction> Transactions = new List<Transaction>();
        public async Task Save(IEnumerable<Transaction> transactions)
        {
            await Task.Delay(1000);
            Transactions.AddRange(transactions);
        }
    }
}