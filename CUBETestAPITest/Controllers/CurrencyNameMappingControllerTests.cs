using NUnit.Framework;
using Moq;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using CUBETestAPI.Repository.Interfaces;
using CUBETestAPI.Controllers;
using CUBETestAPI.Models.ControllerModels;
using CUBETestAPI.Models.ResponseModels;
using CUBETestAPI.Helpers;

namespace CUBETestAPITest.Controllers
{
    [TestFixture]
    public class CurrencyNameMappingControllerTests
    {
        private Mock<IDatabaseService> _mockDatabaseService;
        private CurrencyNameMappingController _controller;

        [SetUp]
        public void Setup()
        {
            _mockDatabaseService = new Mock<IDatabaseService>();
            _controller = new CurrencyNameMappingController(_mockDatabaseService.Object);
        }

        /// <summary>
        /// Create should return created result
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task CreateCurrencyNameMapping_ShouldReturnCreatedResult_WhenValidDataProvided()
        {

            // Arrange
            var currencyID = Guid.NewGuid();
            var inputModel = new CurrencyNameMappingInputModel()
            {
                Currency = "USD",
                ChineseName = "美元"
            };

            var mappedModel = new CurrencyNameMappingModel()
            {
                ID = currencyID,
                Currency = inputModel.Currency,
                ChineseName = inputModel.ChineseName
            };

            _mockDatabaseService.Setup(service => service.CreateCurrencyNameMapping(It.IsAny<CurrencyNameMappingModel>())).ReturnsAsync(currencyID);

            // Act
            var result = await _controller.CreateCurrencyNameMapping(inputModel);

            // Assert 
            result.Result.Should().BeOfType<CreatedAtActionResult>();

            var actionResult = result.Result as CreatedAtActionResult;
            actionResult.Should().NotBeNull();
            actionResult.StatusCode.Should().Be(201); // HTTP 201 Created
            actionResult.Value.Should().BeEquivalentTo(mappedModel, options => options.Excluding(m => m.ID));
            actionResult.RouteValues["id"].Should().Be(currencyID);

            _mockDatabaseService.Verify(service => service.CreateCurrencyNameMapping(It.Is<CurrencyNameMappingModel>(m => m.Currency == inputModel.Currency && 
                                                                                                                          m.ChineseName == inputModel.ChineseName)), Times.Once);
        }

        /// <summary>
        /// Get all should return OK
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task GetAllCurrencyNameMapping_ShouldReturnOK_WhenCurrenciesExist()
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
            var result = await _controller.GetCurrencyNameMapping();

            // Assert
            var okResult = result.Result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be(200);
            okResult.Value.Should().BeEquivalentTo(currencyNameMappings);
        }

        /// <summary>
        /// Get currency by id should return OK
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task GetCurrencyNameMappingByID_ShouldReturnOK_WhenCurrencyExist()
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
            var result = await _controller.GetCurrencyNameMapping(currencyID);

            // Assert
            var okResult = result.Result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be(200);
            okResult.Value.Should().BeEquivalentTo(currencyNameMapping);
        }

        /// <summary>
        /// Update Currency should return NoContent
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task UpdateCurrencyNameMapping_ShouldReturnNoContent_WhenCurrencyExist()
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
            var result = await _controller.UpdateCurrencyNameMapping(currencyID, currencyNameMapping);

            // Assert
            result.Should().BeOfType<NoContentResult>();
        }

        /// <summary>
        /// Delete Currency should return NoContent
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task DeleteCurrencyNameMapping_ShouldReturnNoContent_WhenCurrencyExist()
        {
            // Arrange
            var currencyID = Guid.NewGuid();

            _mockDatabaseService.Setup(service => service.DeleteCurrencyNameMapping(currencyID)).ReturnsAsync(1);

            // Act
            var result = await _controller.DeleteCurrencyNameMapping(currencyID);

            // Assert
            result.Should().BeOfType<NoContentResult>();
        }
    }
}
