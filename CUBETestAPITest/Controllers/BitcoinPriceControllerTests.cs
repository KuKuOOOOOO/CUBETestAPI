using CUBETestAPI.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Moq.Protected;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CUBETestAPITest.Controllers
{
    [TestFixture]
    public class BitcoinPriceControllerTests
    {
        private Mock<HttpMessageHandler> _mockMessageHandler;
        private Mock<IHttpClientFactory> _mockHttpClientFactory;
        private HttpClient _mockHttpClient;
        private BitcoinPriceController _controller;

        [SetUp]
        public void Setup()
        {
            _mockMessageHandler = new Mock<HttpMessageHandler>();
            _mockMessageHandler.Protected().Setup<Task<HttpResponseMessage>>("SendAsync", // Mock the SendAsync method
                                                                             ItExpr.IsAny<HttpRequestMessage>(),
                                                                             ItExpr.IsAny<CancellationToken>()).ReturnsAsync(new HttpResponseMessage
                                                                             {
                                                                                 StatusCode = System.Net.HttpStatusCode.OK,
                                                                                 Content = new StringContent(@"{ ""time"": { ""updatedISO"": ""2025-01-01T00:00:00Z"" }, ""bpi"": { ""USD"": { ""rate"": ""34,567.1234"", ""rate_float"": 34567.1234 } } }")
                                                                             });
            
            _mockHttpClient = new HttpClient(_mockMessageHandler.Object);

            _mockHttpClientFactory = new Mock<IHttpClientFactory>();
            _mockHttpClientFactory.Setup(factory => factory.CreateClient(It.IsAny<string>())).Returns(_mockHttpClient);

            _controller = new BitcoinPriceController(_mockHttpClientFactory.Object);
        }

        [TearDown]
        public void TearDown()
        {
            // 測試完畢清除HttpClient的記憶體
            _mockHttpClient.Dispose();
        }

        /// <summary>
        /// Get Bitcoin price should return OK
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task GetBitcoinPrice_ShouldReturnOK_WhenApiReturnsValidData()
        {
            // Act
            var result = await _controller.GetBitcoinPrice();

            //Assert
            var actionResult = result as OkObjectResult;
            actionResult.Should().NotBeNull();
            actionResult.StatusCode.Should().Be(200);
        }
    }
}
