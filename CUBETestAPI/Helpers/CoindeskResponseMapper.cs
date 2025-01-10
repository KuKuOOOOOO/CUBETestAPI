using CUBETestAPI.Models.InputModels;
using CUBETestAPI.Models.OutputModels;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace CUBETestAPI.Helpers
{
    public static class CoindeskResponseMapper
    {
        public static TransformedCoindeskResponseModel MapToTransformedFormat(CoindeskResponseModel coindeskResponseModel)
        {
            TransformedCoindeskResponseModel transferModel = new TransformedCoindeskResponseModel();

            if (coindeskResponseModel == null)
                return transferModel;



            transferModel = new TransformedCoindeskResponseModel()
            {
                UpdateTime = GetUpdateTime(coindeskResponseModel.Time.UpdatedISO),
                CurrencyInfos = GetCurrencyInfo(coindeskResponseModel.Bpi)
            };

            return transferModel;

        }
        private static string GetUpdateTime(string updatedISO)
        {
            return DateTime.TryParse(updatedISO, out DateTime result) ?
                   result.ToString("yyyy/MM/dd HH:mm:ss", CultureInfo.InvariantCulture) :
                   updatedISO;
        }
        private static List<Models.OutputModels.CurrencyInfo> GetCurrencyInfo(BpiInfo bpiInfo)
        {
            List<Models.OutputModels.CurrencyInfo> currencyInfos = new List<Models.OutputModels.CurrencyInfo>();

            if (bpiInfo == null)
                return currencyInfos;

            if (bpiInfo.USD != null)
            {
                currencyInfos.Add(new Models.OutputModels.CurrencyInfo()
                {
                    Currency = "USD",
                    Currency_ChineseName = "美金",
                    Rate = bpiInfo.USD.Rate,
                    Ratefloat = bpiInfo.USD.RateFloat
                });
            }
            if (bpiInfo.EUR != null)
            {
                currencyInfos.Add(new Models.OutputModels.CurrencyInfo()
                {
                    Currency = "EUR",
                    Currency_ChineseName = "歐元",
                    Rate = bpiInfo.EUR.Rate,
                    Ratefloat = bpiInfo.EUR.RateFloat
                });
            }
            if (bpiInfo.GBP != null)
            {
                currencyInfos.Add(new Models.OutputModels.CurrencyInfo()
                {
                    Currency = "GBP",
                    Currency_ChineseName = "英鎊",
                    Rate = bpiInfo.GBP.Rate,
                    Ratefloat = bpiInfo.GBP.RateFloat
                });
            }
            
            return currencyInfos;
        }
    }
}
