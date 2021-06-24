namespace TransactionUploader.Common
{
    public interface IExtractorManager
    {
        IExtractor GetExtractor(string fileType);
    }
}