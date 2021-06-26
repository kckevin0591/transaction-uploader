using TransactionUploader.Common;

namespace TransactionUploader.WebApp.Models
{
    public static class ConversionExtensions
    {
        public static ApiTransactionModel ConvertToApiTransactionModel(this Transaction txn)
        {
            return new ApiTransactionModel()
            {
                Id = txn.TransactionId,
                Status = txn.Status,
                Payment = $"{txn.Amount} {txn.CurrencyCode}"
            };
        }
    }
}