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

        [Test]
        public async Task CreateCurrencyNameMapping_ShouldReturnCreatedResult_WhenValidDataProvided()
        {

            // Arrange
            var fakeID = Guid.NewGuid();
            var inputModel = new CurrencyNameMappingInputModel()
            {
                Currency = "USD",
                ChineseName = "美元"
            };

            var mappedModel = new CurrencyNameMappingModel()
            {
                ID = fakeID,
                Currency = inputModel.Currency,
                ChineseName = inputModel.ChineseName
            };

            _mockDatabaseService.Setup(service => service.CreateCurrencyNameMapping(It.IsAny<CurrencyNameMappingModel>())).ReturnsAsync(fakeID);

            // Act
            var result = await _controller.CreateCurrencyNameMapping(inputModel);

            // Assert 
            result.Result.Should().BeOfType<CreatedAtActionResult>();

            var actionResult = result.Result as CreatedAtActionResult;
            actionResult.Should().NotBeNull();
            actionResult.StatusCode.Should().Be(201); // HTTP 201 Created
            actionResult.Value.Should().BeEquivalentTo(mappedModel, options => options.Excluding(m => m.ID));
            actionResult.RouteValues["id"].Should().Be(fakeID);

            _mockDatabaseService.Verify(service => service.CreateCurrencyNameMapping(It.Is<CurrencyNameMappingModel>(m => m.Currency == inputModel.Currency && 
                                                                                                                          m.ChineseName == inputModel.ChineseName)), Times.Once);

        }
    }
}
