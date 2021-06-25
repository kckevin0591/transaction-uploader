using System.Xml.Serialization;

namespace TransactionUploader.Common.Extractors.XML
{
    public class XmlPaymentDetails
    {
        [XmlElement] 
        public decimal? Amount { get; set; }
        [XmlElement] 
        public string CurrencyCode { get; set; }
    }
}