using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TransactionUploader.Common.Extractors.XML;

namespace TransactionUploader.Common.Tests
{
    [TestClass]
    public class XmlExtractorTests
    {
        [TestMethod]
        public void ValidXml_Success()
        {
            var data = "<Transactions>\r\n" +
                       "<Transaction id=\"Inv00001\">\r\n<TransactionDate>2019-01-23T13:45:10</TransactionDate>\r\n<PaymentDetails>\r\n<Amount>200.00</Amount>\r\n<CurrencyCode>USD</CurrencyCode>\r\n</PaymentDetails>\r\n<Status>Done</Status>\r\n</Transaction>\r\n" +
                       "<Transaction id=\"Inv00002\">\r\n<TransactionDate>2019-01-24T16:09:15</TransactionDate>\r\n<PaymentDetails>\r\n<Amount>10000.00</Amount>\r\n<CurrencyCode>EUR</CurrencyCode>\r\n</PaymentDetails>\r\n<Status>Rejected</Status>\r\n</Transaction>\r\n" +
                       "</Transactions>";
            var extractor = new XmlExtractor();
            var txns = extractor.Extract(data);

            Assert.IsNotNull(txns,"txns != null");
            Assert.AreEqual(2,txns.Count());
        }
    }
}
