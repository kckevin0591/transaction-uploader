using System.Collections.Generic;
using System.Threading.Tasks;
using TransactionUploader.Common;

namespace TransactionUploader.Repository
{
    public interface ITransactionRepository
    {
        Task<RepositoryStatus> Save(IEnumerable<Transaction> transactions);
    }
}