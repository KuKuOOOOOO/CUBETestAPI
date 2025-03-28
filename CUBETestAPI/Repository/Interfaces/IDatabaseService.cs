﻿using CUBETestAPI.Models.ResponseModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CUBETestAPI.Repository.Interfaces
{
    public interface IDatabaseService
    {
        Task<IEnumerable<CurrencyNameMappingModel>> GetAllCurrencyNameMapping();
        Task<CurrencyNameMappingModel> GetCurrencyNameMapping(Guid id);
        Task<Guid> CreateCurrencyNameMapping(CurrencyNameMappingModel currencyNameMapping);
        Task<int> UpdateCurrencyNameMapping(CurrencyNameMappingModel currencyNameMapping);
        Task<int> DeleteCurrencyNameMapping(Guid id);
    }
}
