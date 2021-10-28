using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DataModel
{
    public class ChartJsData<T>
    {
        [JsonPropertyName("labels")] public List<string> Labels { get; set; }

        [JsonPropertyName("datasets")] public List<DataSet<T>> DataSets { get; set; }
    }
}