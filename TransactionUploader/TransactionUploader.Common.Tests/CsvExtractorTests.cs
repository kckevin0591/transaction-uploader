using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TransactionUploader.Common.Extractors.CSV;

namespace TransactionUploader.Common.Tests
{
    [TestClass]
    public class CsvExtractorTests
    {
        [TestMethod]
        public void ValidCsv_Success()
        {
            var data = "\"Invoice0000001\",\"1,000.00\", \"USD\", \"20/02/2019 12:33:16\", \"Approved\"\r\n" +
                       "\"Invoice0000002\",\"300.00\",\"USD\",\"21/02/2019 02:04:59\", \"Failed\"";
            var extractor = new CsvExtractor();
            var txns = extractor.Extract(data);

            Assert.IsNotNull(txns,"txns != null");
            Assert.AreEqual(2,txns.Count());
        }
    }
}