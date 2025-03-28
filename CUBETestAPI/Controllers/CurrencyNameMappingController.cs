using CUBETestAPI.Helpers;
using CUBETestAPI.Models.ControllerModels;
using CUBETestAPI.Models.ResponseModels;
using CUBETestAPI.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CUBETestAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CurrencyNameMappingController : ControllerBase
    {
        private readonly IDatabaseService _databaseService;
        private readonly RsaCryptoService _rsaService;

        public CurrencyNameMappingController(IDatabaseService databaseService, RsaCryptoService rsaService)
        {
            _databaseService = databaseService;
            _rsaService = rsaService;
        }

        // GET: api/curreny
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CurrencyNameMappingModel>>> GetCurrencyNameMapping()
        {
            IEnumerable<CurrencyNameMappingModel> currencyNameMappings = await _databaseService.GetAllCurrencyNameMapping();
            var json = JsonConvert.SerializeObject(currencyNameMappings);
            var encryptedData = _rsaService.Encrypt(json);
            var data = _rsaService.Decrypt(encryptedData);
            return Ok(new
            {
                Data = data
            });

        }

        // GET: api/currency/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<CurrencyNameMappingModel>> GetCurrencyNameMapping(Guid id)
        {
            CurrencyNameMappingModel currencyNameMapping = await _databaseService.GetCurrencyNameMapping(id);

            if (currencyNameMapping == null)
                return NotFound();

            return Ok(currencyNameMapping);
        }

        // POST: api/currency
        [HttpPost]
        public async Task<ActionResult<CurrencyNameMappingModel>> CreateCurrencyNameMapping(CurrencyNameMappingInputModel inputModel)
        {
            if (inputModel == null)
                return NotFound();
            var currencyNameMapping = CurrencyNameMappingModelMapper.MapToCurrencyNameMapping(inputModel);
            Guid currencyNameMappingID = await _databaseService.CreateCurrencyNameMapping(currencyNameMapping);
            currencyNameMapping.ID = currencyNameMappingID;

            return CreatedAtAction(nameof(GetCurrencyNameMapping), new { id = currencyNameMappingID }, currencyNameMapping);
        }

        // PUT: api/currency/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCurrencyNameMapping(Guid id, CurrencyNameMappingModel currencyNameMapping)
        {
            if (id != currencyNameMapping.ID)
                return BadRequest();

            int rowsAffected = await _databaseService.UpdateCurrencyNameMapping(currencyNameMapping);

            if (rowsAffected == 0)
                return NotFound();

            return NoContent();
        }

        // DELETE: api/currency/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCurrencyNameMapping(Guid id)
        {
            int rowsAffected = await _databaseService.DeleteCurrencyNameMapping(id);

            if (rowsAffected == 0)
                return NotFound();

            return NoContent();
        }

    }
}
