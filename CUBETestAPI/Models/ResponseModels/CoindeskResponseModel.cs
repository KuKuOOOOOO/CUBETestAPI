using Newtonsoft.Json;

namespace CUBETestAPI.Models.ResponseModels
{
    public class CoindeskResponseModel
    {
        [JsonProperty("time")]
        public TimeInfo Time { get; set; }
        [JsonProperty("bpi")]
        public BpiInfo Bpi { get; set; }

    }
    public class TimeInfo
    {
        [JsonProperty("updated")]
        public string Updated { get; set; }

        [JsonProperty("updatedISO")]
        public string UpdatedISO { get; set; }
    }
    public class BpiInfo
    {
        [JsonProperty("USD")]
        public CurrencyInfo USD { get; set; }
        [JsonProperty("GBP")]
        public CurrencyInfo GBP { get; set; }
        [JsonProperty("EUR")]
        public CurrencyInfo EUR { get; set; }
    }
    public class CurrencyInfo
    {
        [JsonProperty("code")]
        public string Code { get; set; }
        [JsonProperty("rate")]
        public string Rate { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("rate_float")]
        public float RateFloat { get; set; }
    }

}
