using System.Collections.Generic;
using System.Xml.Serialization;

namespace TransactionUploader.Common.Extractors.XML
{
    [XmlRoot(ElementName = "Transactions")]
    public class XmlTransactionList
    {
        public XmlTransactionList()
        {
            XmlTransactions = new List<XmlTransaction>();
        }

        [XmlElement("Transaction")]
        public List<XmlTransaction> XmlTransactions { get; set; }
    }
}