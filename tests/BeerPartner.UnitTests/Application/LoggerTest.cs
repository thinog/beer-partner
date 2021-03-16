using System;
using System.IO;
using BeerPartner.Application;
using Moq;
using Xunit;

namespace BeerPartner.UnitTests.Application
{
    public class LoggerTest
    {
        [Fact]
        public void Should_CallWriteLine_When_LogInfoIsCalled()
        {
            // Arrange
            var writerMock = new Mock<TextWriter>();
            var logger = new Logger(writerMock.Object);

            var request = ("test", new {});
            
            // Act
            logger.Info(request.Item1);
            logger.Info(request.Item1, request.Item2);

            // Act - Assert
            writerMock.Verify(output => output.WriteLine(It.IsAny<string>()));
        }
        
        [Fact]
        public void Should_CallWriteLine_When_LogErrorIsCalled()
        {
            // Arrange
            var writerMock = new Mock<TextWriter>();
            var logger = new Logger(writerMock.Object);

            var request = ("test", new Exception());
            
            // Act
            logger.Error(request.Item1);
            logger.Error(request.Item1, request.Item2);

            // Act - Assert
            writerMock.Verify(output => output.WriteLine(It.IsAny<string>()));
        }
    }
}