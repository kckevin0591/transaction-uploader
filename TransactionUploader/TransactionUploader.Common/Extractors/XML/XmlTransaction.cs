using System.Xml.Serialization;

namespace TransactionUploader.Common.Extractors.XML
{
    public class XmlTransaction
    {
        [XmlAttribute]
        public string id { get; set; }
        [XmlElement] 
        public string TransactionDate { get; set; }
        [XmlElement] 
        public XmlPaymentDetails PaymentDetails { get; set; }
        [XmlElement]
        public string Status { get; set; }
    }
}