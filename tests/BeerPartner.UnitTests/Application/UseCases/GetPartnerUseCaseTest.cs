using Xunit;
using Moq;
using BeerPartner.Application.Interfaces;
using System;
using BeerPartner.Application.Interfaces.Repositories;
using BeerPartner.Application.UseCases.GetPartner;
using BeerPartner.UnitTests.Domain.Entities;

namespace BeerPartner.UnitTests.Application.UseCases
{
    public class GetPartnerUseCaseTest
    {
        #region Get
        [Fact]
        public void Should_CallOkOutput_When_GetMethodReceiveAValidRequest()
        {
            // Arrange
            var loggerMock = new Mock<ILogger>();

            var repositoryMock = new Mock<IPartnerRepository>();
            repositoryMock.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(() => PartnerDefaults.ValidPartner);

            var useCase = new GetPartnerUseCase(repositoryMock.Object, loggerMock.Object);
            var outputMock = new Mock<IOutputPort>();

            var request = Guid.NewGuid();
            
            // Act
            useCase.SetOutputPort(outputMock.Object);         
            useCase.Get(request);

            // Act - Assert
            outputMock.Verify(output => output.Ok(It.IsAny<GetPartnerResponse>()));
        }
        
        [Fact]
        public void Should_CallNotFoundOutput_When_GetMethodCantFoundAPartnerWithId()
        {
            // Arrange
            var loggerMock = new Mock<ILogger>();

            var repositoryMock = new Mock<IPartnerRepository>();
            repositoryMock.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(() => null);

            var useCase = new GetPartnerUseCase(repositoryMock.Object, loggerMock.Object);
            var outputMock = new Mock<IOutputPort>();

            var request = Guid.NewGuid();
            
            // Act
            useCase.SetOutputPort(outputMock.Object);            
            useCase.Get(request);

            // Act - Assert
            outputMock.Verify(output => output.NotFound());
        }
        
        [Fact]
        public void Should_CallErrorOutput_When_GetMethodDealWithAnException()
        {
            // Arrange
            var loggerMock = new Mock<ILogger>();

            var repositoryMock = new Mock<IPartnerRepository>();
            repositoryMock.Setup(x => x.GetById(It.IsAny<Guid>())).Throws<Exception>();

            var useCase = new GetPartnerUseCase(repositoryMock.Object, loggerMock.Object);
            var outputMock = new Mock<IOutputPort>();

            var request = Guid.NewGuid();
            
            // Act
            useCase.SetOutputPort(outputMock.Object);            
            useCase.Get(request);

            // Act - Assert
            outputMock.Verify(output => output.Error(It.IsAny<string>()));
        }
        #endregion

        #region Search
        [Fact]
        public void Should_CallOkOutput_When_SearchMethodReceiveAValidRequest()
        {
            // Arrange
            var loggerMock = new Mock<ILogger>();

            var repositoryMock = new Mock<IPartnerRepository>();
            repositoryMock.Setup(x => x.GetNearest(It.IsAny<double>(), It.IsAny<double>())).Returns(() => PartnerDefaults.ValidPartner);

            var useCase = new GetPartnerUseCase(repositoryMock.Object, loggerMock.Object);
            var outputMock = new Mock<IOutputPort>();

            var request = (0, 0);
            
            // Act
            useCase.SetOutputPort(outputMock.Object);         
            useCase.Search(request.Item1, request.Item2);

            // Act - Assert
            outputMock.Verify(output => output.Ok(It.IsAny<GetPartnerResponse>()));
        }
        
        [Fact]
        public void Should_CallNotFoundOutput_When_SearchMethodCantFoundAPartner()
        {
            // Arrange
            var loggerMock = new Mock<ILogger>();

            var repositoryMock = new Mock<IPartnerRepository>();
            repositoryMock.Setup(x => x.GetNearest(It.IsAny<double>(), It.IsAny<double>())).Returns(() => null);

            var useCase = new GetPartnerUseCase(repositoryMock.Object, loggerMock.Object);
            var outputMock = new Mock<IOutputPort>();

            var request = (0, 0);
            
            // Act
            useCase.SetOutputPort(outputMock.Object);            
            useCase.Search(request.Item1, request.Item2);

            // Act - Assert
            outputMock.Verify(output => output.NotFound());
        }
        
        [Fact]
        public void Should_CallInvalidOutput_When_SearchMethodReceiveInvalidParams()
        {
            // Arrange
            var loggerMock = new Mock<ILogger>();

            var repositoryMock = new Mock<IPartnerRepository>();
            repositoryMock.Setup(x => x.GetNearest(It.IsAny<double>(), It.IsAny<double>())).Returns(() => null);

            var useCase = new GetPartnerUseCase(repositoryMock.Object, loggerMock.Object);
            var outputMock = new Mock<IOutputPort>();

            var request = (200, 200);
            
            // Act
            useCase.SetOutputPort(outputMock.Object);            
            useCase.Search(request.Item1, request.Item2);

            // Act - Assert
            outputMock.Verify(output => output.Invalid(It.IsAny<string>()));
        }
        
        [Fact]
        public void Should_CallErrorOutput_When_SearchMethodDealWithAnException()
        {
            // Arrange
            var loggerMock = new Mock<ILogger>();

            var repositoryMock = new Mock<IPartnerRepository>();
            repositoryMock.Setup(x => x.GetNearest(It.IsAny<double>(), It.IsAny<double>())).Throws<Exception>();

            var useCase = new GetPartnerUseCase(repositoryMock.Object, loggerMock.Object);
            var outputMock = new Mock<IOutputPort>();

            var request = (0, 0);
            
            // Act
            useCase.SetOutputPort(outputMock.Object);            
            useCase.Search(request.Item1, request.Item2);

            // Act - Assert
            outputMock.Verify(output => output.Error(It.IsAny<string>()));
        }
        #endregion
    }
}