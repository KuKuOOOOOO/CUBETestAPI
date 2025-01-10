using NUnit.Framework;
using Moq;
using System;
using System.Threading.Tasks;
using FluentAssertions;
using CUBETestAPI.Repository.Interfaces;
using CUBETestAPI.Repository.Data;
using CUBETestAPI.Models.ResponseModels;

namespace CUBETestAPITest.Services
{
    [TestFixture]
    public class CurrencyNameMappingServiceTests
    {
        private Mock<IDatabaseService> _mockDatabaseService;

        [SetUp]
        public void Setup()
        {
            _mockDatabaseService = new Mock<IDatabaseService>();
        }

        [Test]
        public async Task CreateCurrencyNameMapping_ShouldReturnGuid_WhenDataIsValid()
        {

            // Arrange
            var fakeID = Guid.NewGuid();
            var model = new CurrencyNameMappingModel()
            {
                ID = fakeID,
                Currency = "USD",
                ChineseName = "美元"
            };

            _mockDatabaseService.Setup(service => service.CreateCurrencyNameMapping(It.Is<CurrencyNameMappingModel>(m => m.ID == fakeID && 
                                                                                                                         m.Currency == "USD" && 
                                                                                                                         m.ChineseName == "美元"))).ReturnsAsync(fakeID);

            // Act
            var result = await _mockDatabaseService.Object.CreateCurrencyNameMapping(model);

            //Assert
            result.Should().Be(fakeID);
            _mockDatabaseService.Verify(service => service.CreateCurrencyNameMapping(It.IsAny<CurrencyNameMappingModel>()), Times.Once);
        }
    }
}
