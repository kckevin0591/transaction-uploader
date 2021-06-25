using TinyCsvParser.Mapping;

namespace TransactionUploader.Common.Extractors.CSV
{
    public class CsvTransactionMapping:CsvMapping<CsvTransaction>
    {
        public CsvTransactionMapping():base()
        {
            MapProperty(0, x => x.TransactionId);
            MapProperty(1, x => x.Amount);
            MapProperty(2, x => x.CurrencyCode);
            MapProperty(3, x => x.TransactionDate);
            MapProperty(4, x => x.Status);
        }
    }
}