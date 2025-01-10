namespace CUBETestAPI.Models.OutputModels
{
    public class TransformedCoindeskResponseModel
    {
        public string UpdateTime { get; set; }
        public List<CurrencyInfo> CurrencyInfos { get; set; }
    }
    public class CurrencyInfo
    {
        public string Currency { get; set; }
        public string Currency_ChineseName { get; set; }
        public string Rate { get; set; }
        public float Ratefloat { get; set; }
    }
}
