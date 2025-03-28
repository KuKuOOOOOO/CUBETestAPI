using CUBETestAPI.Helpers;
using CUBETestAPI.Models.InputModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace CUBETestAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BitcoinPriceController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly RsaCryptoService _rsaService;

        public BitcoinPriceController(IHttpClientFactory httpClientFactory, RsaCryptoService rsaService)
        {
            _httpClient = httpClientFactory.CreateClient();
            _rsaService = rsaService;
        }
        [HttpGet("current")]
        public async Task<IActionResult> GetBitcoinPrice()
        {
            const string coindeskUrl = "https://api.coindesk.com/v1/bpi/currentprice.json";

            try
            {
                var response = await _httpClient.GetAsync(coindeskUrl);

                if (!response.IsSuccessStatusCode)
                    return StatusCode((int)response.StatusCode, "Failed to fetch data from Coindesk API.");

                var jsonResponse = await response.Content.ReadAsStringAsync();
                var coindeskData = JsonConvert.DeserializeObject<CoindeskResponseModel>(jsonResponse);

                if (coindeskData == null)
                    return StatusCode(500, "Error parsing Coindesk API response.");

                var transformedData = CoindeskResponseMapper.MapToTransformedFormat(coindeskData);
                var json = JsonConvert.SerializeObject(transformedData);
                var encryptedData = _rsaService.Encrypt(json);

                return Ok(new
                {
                    EncryptedData = encryptedData
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
