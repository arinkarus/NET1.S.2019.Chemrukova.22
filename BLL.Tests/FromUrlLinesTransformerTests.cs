using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Implementation;
using BLL.Implementation.Entities;
using BLL.Interface.Logger;
using BLL.Interface;
using BLL.Interface.Validation;
using Moq;
using NUnit;
using NUnit.Framework;

namespace BLL.Tests
{
    public class FromUrlLinesTransformerTests
    {
        private static Mock<ITransformer<string, URLAddress>> mockForParser;
        private static Mock<IValidator<string>> mockForValidator;
        private static Mock<ILogger> mockForLogger;

        [SetUp]
        public void FromUrlLinesDeserializerTests_SetUp()
        {
            mockForParser = new Mock<ITransformer<string, URLAddress>>();
            mockForValidator = new Mock<IValidator<string>>();
            mockForLogger = new Mock<ILogger>();
        }

        [TestCase("string doesn't match pattern")]
        public void Transform_Calls_Validator_And_Logger(string value)
        {
            var stringList = new List<string> { value };
            mockForValidator.Setup(x => x.IsValid(stringList[0])).Returns(false);
            mockForLogger.Setup(x => x.Error(It.IsAny<string>()));
            var fromUrlLinesTransformer = new FromUrlLinesTransformer(mockForValidator.Object,
                mockForParser.Object, mockForLogger.Object);
            fromUrlLinesTransformer.Transform(stringList);
            var uRLAddresses = fromUrlLinesTransformer.Transform(stringList);
            foreach (var item in uRLAddresses) { }
            mockForParser.Verify(x => x.Transform(stringList[0]), Times.Never);
            mockForLogger.Verify(x => x.Error(It.IsAny<string>()), Times.Once);
            mockForValidator.Verify(x => x.IsValid(stringList[0]), Times.Once);
        }

        [TestCase("https://habrahabr.ru/company/it-grad/blog/341486/")]
        public void Transform_Calls_Parser_And_Validator(string value)
        {
            var stringList = new List<string>() { value };
            mockForValidator.Setup(x => x.IsValid(stringList[0])).Returns(true);
            var fromUrlLinesTransformer = new FromUrlLinesTransformer(mockForValidator.Object,
               mockForParser.Object, mockForLogger.Object);
            var uRLAddresses = fromUrlLinesTransformer.Transform(stringList);
            foreach (var item in uRLAddresses) {}
            mockForValidator.Verify(x => x.IsValid(stringList[0]), Times.Once);
            mockForParser.Verify(x => x.Transform(stringList[0]), Times.Once);
            mockForLogger.Verify(x => x.Error(It.IsAny<string>()), Times.Never);
        }    

        [Test]
        public void Transform_Calls_Parser_And_Validator()
        {
            var stringList = new List<string>()
            { "https://habrahabr.ru/company/it-grad/blog/341486/",
               "https://github.com/AnzhelikaKravchuk/2017-2018.MMF.BSU"
            };

            mockForParser.Setup(x => x.Transform(It.IsAny<string>()));
            mockForValidator.Setup(x => x.IsValid(It.IsAny<string>())).Returns(true);
            mockForLogger.Setup(x => x.Error(It.IsAny<string>()));
            var fromUrlLinesTransformer = new FromUrlLinesTransformer(mockForValidator.Object,
                mockForParser.Object, mockForLogger.Object);
            var uRLAddresses = fromUrlLinesTransformer.Transform(stringList);
            foreach (var item in uRLAddresses) { }
            mockForLogger.Verify(x => x.Error(It.IsAny<string>()), times: Times.Never);
            mockForValidator.Verify(x => x.IsValid(It.IsAny<string>()),
                times: Times.Exactly(2));
            mockForParser.Verify(x => x.Transform(It.IsAny<string>()), times: Times.Exactly(2));
        }
    } 
}
