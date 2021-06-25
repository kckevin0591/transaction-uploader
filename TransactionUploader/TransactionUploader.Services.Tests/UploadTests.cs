using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TransactionUploader.Common;
using TransactionUploader.Repository;

namespace TransactionUploader.Services.Tests
{
    [TestClass]
    public class UploadTests
    {
        private readonly Mock<ITransactionRepository> _transactionRepository = new Mock<ITransactionRepository>();
        private readonly Mock<IExtractorManager> _extractorManager = new Mock<IExtractorManager>();

        private void Setup()
        {
            Mock<IExtractor> extractor = new Mock<IExtractor>();
            extractor.Setup(e => e.Extract(It.IsAny<string>()))
                .Returns(new List<Transaction>());

            _extractorManager.Setup(em => em.GetExtractor(It.IsAny<string>()))
                .Returns(extractor.Object);

        }

        [TestMethod]
        public async Task Upload_Success()
        {
            Setup();

            var uploadService = new TransactionUploaderService(_transactionRepository.Object, _extractorManager.Object);
            await uploadService.Upload("", "");
        }
    }
}
