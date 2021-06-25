using TransactionUploader.Common.Exceptions;
using TransactionUploader.Common.Extractors;
using TransactionUploader.Common.Extractors.XML;

namespace TransactionUploader.Common
{
    public class ExtractorManager : IExtractorManager
    {
        public IExtractor GetExtractor(string fileType)
        {
            //for simplicity we just use if-else
            if (fileType == "xml")
                return new XmlExtractor();

            throw new InvalidFileTypeException($"No extractor found for {fileType} file");
        }
    }
}