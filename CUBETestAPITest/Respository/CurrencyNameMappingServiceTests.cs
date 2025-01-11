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

        /// <summary>
        /// Create currency should return guid in db
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task CreateCurrencyNameMapping_ShouldReturnGuid_WhenDataIsValid()
        {

            // Arrange
            var currencyID = Guid.NewGuid();
            var model = new CurrencyNameMappingModel()
            {
                ID = currencyID,
                Currency = "USD",
                ChineseName = "美元"
            };

            _mockDatabaseService.Setup(service => service.CreateCurrencyNameMapping(It.Is<CurrencyNameMappingModel>(m => m.ID == currencyID && 
                                                                                                                         m.Currency == "USD" && 
                                                                                                                         m.ChineseName == "美元"))).ReturnsAsync(currencyID);

            // Act
            var result = await _mockDatabaseService.Object.CreateCurrencyNameMapping(model);

            //Assert
            result.Should().Be(currencyID);
            _mockDatabaseService.Verify(service => service.CreateCurrencyNameMapping(It.IsAny<CurrencyNameMappingModel>()), Times.Once);
        }

        /// <summary>
        /// Get all currency should return currency
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task GetAllCurrencyNameMapping_ShouldReturnCurrencyNameMapping_WhenCurrenciesExist()
        {
            // Arrange
            var currencyNameMappings = new List<CurrencyNameMappingModel>()
            {
                new CurrencyNameMappingModel
                {
                    ID = Guid.NewGuid(),
                    Currency = "USD",
                    ChineseName = "美金"
                },
                new CurrencyNameMappingModel
                {
                    ID = Guid.NewGuid(),
                    Currency = "EUR",
                    ChineseName = "歐元"
                }
            };

            _mockDatabaseService.Setup(service => service.GetAllCurrencyNameMapping()).ReturnsAsync(currencyNameMappings);

            // Act
            var result = await _mockDatabaseService.Object.GetAllCurrencyNameMapping();

            // Assert
            result.Should().BeEquivalentTo(currencyNameMappings);
        }

        /// <summary>
        /// Get currency by ID should return currency
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task GetCurrencyNameMappingByID_ShouldReturnCurrencyNameMapping_WhenCurrencyExist()
        {
            // Arrange
            var currencyID = Guid.NewGuid();
            var currencyNameMapping = new CurrencyNameMappingModel()
            {
                ID = currencyID,
                Currency = "USD",
                ChineseName = "美金"
            };

            _mockDatabaseService.Setup(service => service.GetCurrencyNameMapping(currencyID)).ReturnsAsync(currencyNameMapping);

            // Act
            var result = await _mockDatabaseService.Object.GetCurrencyNameMapping(currencyID);

            // Assert
            result.Should().BeEquivalentTo(currencyNameMapping);
            _mockDatabaseService.Verify(service => service.GetCurrencyNameMapping(currencyID), Times.Once);
        }

        /// <summary>
        /// Update currency should return one
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task UpdateCurrencyNameMapping_ShouldReturnOne_WhenUpdateIsSuccessful()
        {
            // Arrange
            var currencyID = Guid.NewGuid();
            var currencyNameMapping = new CurrencyNameMappingModel()
            {
                ID = currencyID,
                Currency = "TWD",
                ChineseName = "新台幣"
            };

            _mockDatabaseService.Setup(service => service.UpdateCurrencyNameMapping(It.IsAny<CurrencyNameMappingModel>())).ReturnsAsync(1);

            // Act
            var result =  await _mockDatabaseService.Object.UpdateCurrencyNameMapping(currencyNameMapping);

            // Assert
            result.Should().Be(1);
            _mockDatabaseService.Verify(service => service.UpdateCurrencyNameMapping(currencyNameMapping), Times.Once);
        }

        /// <summary>
        /// Delete currency should return one
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task DeleteCurrencyNameMapping_ShouldReturnOne_WhenDeleteIsSuccessful()
        {
            // Arrange
            var currencyID = Guid.NewGuid();

            _mockDatabaseService.Setup(service => service.DeleteCurrencyNameMapping(currencyID)).ReturnsAsync(1);

            // Act
            var result = await _mockDatabaseService.Object.DeleteCurrencyNameMapping(currencyID);

            // Assert
            result.Should().Be(1);
            _mockDatabaseService.Verify(service => service.DeleteCurrencyNameMapping(currencyID), Times.Once);
        }
    }
}
