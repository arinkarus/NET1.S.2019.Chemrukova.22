using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Moq;
using BLL.Interface;
using BLL.Implementation.Entities;
using System.Xml.Linq;
using BLL.Interface.Validation;
using BLL.Interface.Logger;
using BLL.Implementation.Service;

namespace BLL.Tests
{
    public class XmlDocumentServiceTests
    {
        private static Mock<IDataPersister<XDocument>> mockForDataPersister;
        private static Mock<IDataRetriever<IEnumerable<string>>> mockForDataRetriever;
        private static Mock<ITransformer<IEnumerable<string>,
        IEnumerable<URLAddress>>> mockForStringToUrlAddressesTransformer;
        private static Mock<ITransformer<IEnumerable<URLAddress>, XDocument>> mockForUrlAddressesToXDocumentTransformer;
        private static Mock<ITransformer<string, URLAddress>> mockForParser;
        private static Mock<IValidator<string>> mockForValidator;
        private static Mock<ILogger> mockForLogger;

        [SetUp]
        public void XmlDocumentServiceTests_SetUp()
        {
            mockForDataPersister = new Mock<IDataPersister<XDocument>>();
            mockForDataRetriever = new Mock<IDataRetriever<IEnumerable<string>>>();
            mockForStringToUrlAddressesTransformer = new Mock<ITransformer<IEnumerable<string>, IEnumerable<URLAddress>>>();
            mockForParser = new Mock<ITransformer<string, URLAddress>>();
            mockForValidator = new Mock<IValidator<string>>();
            mockForLogger = new Mock<ILogger>();
            mockForUrlAddressesToXDocumentTransformer = new Mock<ITransformer<IEnumerable<URLAddress>, XDocument>>();
        }

        [Test]
        public void Proccess_Calls_Retriever_Persister_Serializer_Deserializer()
        {
            var stringList = new List<string>()
            { "https://habrahabr.ru/company/it-grad/blog/341486/",
               "https://github.com/AnzhelikaKravchuk/2017-2018.MMF.BSU"
            };
            URLAddress firstUrlAddress = new URLAddress("habrahabr.ru",
                new List<string> { "company", "it-grad", "blog", "341486" });
            URLAddress secondUrlAddress = new URLAddress("github.com",
                new List<string> { "AnzhelikaKravchuk", "2017-2018.MMF.BSU" });
            mockForDataPersister.Setup(s => s.Save(It.IsAny<XDocument>()));
            mockForDataRetriever.Setup(s => s.Load()).Returns(stringList);
            var urlAddresses = new List<URLAddress>() { firstUrlAddress, secondUrlAddress };
            mockForStringToUrlAddressesTransformer.Setup(s => s.Transform(stringList)).Returns(urlAddresses);
            mockForParser.Setup(s => s.Transform(It.IsAny<string>()));
            mockForValidator.Setup(s => s.IsValid(It.IsAny<string>()));
            mockForUrlAddressesToXDocumentTransformer.Setup(s => s.Transform(urlAddresses));
            mockForLogger.Setup(s => s.Error(It.IsAny<string>()));
            var service = new XmlDocumentService(mockForDataPersister.Object, mockForDataRetriever.Object,
                mockForStringToUrlAddressesTransformer.Object, mockForUrlAddressesToXDocumentTransformer.Object);
            service.Process();
            mockForDataRetriever.Verify(s => s.Load(), Times.Once);
            mockForStringToUrlAddressesTransformer.Verify(s => s.Transform(stringList), Times.Once);
            mockForUrlAddressesToXDocumentTransformer.Verify(s => s.Transform(urlAddresses), Times.Once);
            mockForDataPersister.Verify(s => s.Save(It.IsAny<XDocument>()), Times.Once);
        }
    }
}
