using System.Collections.Generic;

namespace TransactionUploader.Common
{
    public interface IExtractor
    {
        IEnumerable<Transaction> Extract(string data);
    }
}