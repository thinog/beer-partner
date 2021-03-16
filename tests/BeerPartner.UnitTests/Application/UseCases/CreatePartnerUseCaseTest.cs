using Xunit;
using Moq;
using BeerPartner.Application.Interfaces;
using System;
using BeerPartner.Application.Interfaces.Repositories;
using BeerPartner.Domain.Entities;
using BeerPartner.Application.UseCases.CreatePartner;
using BeerPartner.UnitTests.Domain.Entities;

namespace BeerPartner.UnitTests.Application.UseCases
{
    public class CreatePartnerUseCaseTest
    {
        [Fact]
        public void Should_CallOkOutput_When_ReceiveAValidRequest()
        {
            // Arrange
            var loggerMock = new Mock<ILogger>();

            var repositoryMock = new Mock<IPartnerRepository>();
            repositoryMock.Setup(x => x.Insert(It.IsAny<Partner>()));

            var useCase = new CreatePartnerUseCase(repositoryMock.Object, loggerMock.Object);
            var outputMock = new Mock<IOutputPort>();

            var request = new CreatePartnerRequest
            {
                TradingName = PartnerDefaults.ValidPartner.TradingName,
                OwnerName = PartnerDefaults.ValidPartner.OwnerName,
                Address = PartnerDefaults.ValidPartner.Address,
                CoverageArea = PartnerDefaults.ValidPartner.CoverageArea,
            };
            
            // Act
            useCase.SetOutputPort(outputMock.Object);            
            useCase.Execute(request);

            // Act - Assert
            outputMock.Verify(output => output.Ok(It.IsAny<Guid>()));
        }
        
        [Fact]
        public void Should_CallInvalidOutput_When_ReceiveAnInvalidRequest()
        {
            // Arrange
            var loggerMock = new Mock<ILogger>();

            var repositoryMock = new Mock<IPartnerRepository>();
            repositoryMock.Setup(x => x.Insert(It.IsAny<Partner>()));

            var useCase = new CreatePartnerUseCase(repositoryMock.Object, loggerMock.Object);
            var outputMock = new Mock<IOutputPort>();

            var request = new CreatePartnerRequest
            {
                TradingName = PartnerDefaults.InvalidPartner.TradingName,
                OwnerName = PartnerDefaults.InvalidPartner.OwnerName,
                Address = PartnerDefaults.InvalidPartner.Address,
                CoverageArea = PartnerDefaults.InvalidPartner.CoverageArea,
            };
            
            // Act
            useCase.SetOutputPort(outputMock.Object);            
            useCase.Execute(request);

            // Act - Assert
            outputMock.Verify(output => output.Invalid(It.IsAny<string>()));
        }
        
        [Fact]
        public void Should_CallErrorOutput_When_DealWithAnException()
        {
            // Arrange
            var loggerMock = new Mock<ILogger>();

            var repositoryMock = new Mock<IPartnerRepository>();
            repositoryMock.Setup(x => x.Insert(It.IsAny<Partner>())).Throws<Exception>();

            var useCase = new CreatePartnerUseCase(repositoryMock.Object, loggerMock.Object);
            var outputMock = new Mock<IOutputPort>();

            var request = new CreatePartnerRequest
            {
                TradingName = PartnerDefaults.ValidPartner.TradingName,
                OwnerName = PartnerDefaults.ValidPartner.OwnerName,
                Address = PartnerDefaults.ValidPartner.Address,
                CoverageArea = PartnerDefaults.ValidPartner.CoverageArea,
            };
            
            // Act
            useCase.SetOutputPort(outputMock.Object);            
            useCase.Execute(request);

            // Act - Assert
            outputMock.Verify(output => output.Error(It.IsAny<string>()));
        }
    }
}