using CUBETestAPI.Models.ControllerModels;
using CUBETestAPI.Models.ResponseModels;

namespace CUBETestAPI.Helpers
{
    public static class CurrencyNameMappingModelMapper
    {
        public static CurrencyNameMappingModel MapToCurrencyNameMapping(CurrencyNameMappingInputModel inputModel)
        {
            CurrencyNameMappingModel currencyNameMapping = new CurrencyNameMappingModel();
            
            if(inputModel == null) 
                return currencyNameMapping;

            currencyNameMapping = new CurrencyNameMappingModel()
            {
                ID = Guid.NewGuid(),
                Currency = inputModel.Currency,
                ChineseName = inputModel.ChineseName
            };

            return currencyNameMapping;
        }
    }
}
