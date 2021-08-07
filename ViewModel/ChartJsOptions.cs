using System.Text.Json.Serialization;
using WebApplication.Models.Contracts;

namespace DataModel
{
    public class ChartJsOptions<T> : IChartJsOptions
    {
        [JsonPropertyName("type")] public string Type { get; set; }

        [JsonPropertyName("data")] public ChartJsData<T> Data { get; set; }

        [JsonPropertyName("options")] public object Options { get; set; }
    }
}