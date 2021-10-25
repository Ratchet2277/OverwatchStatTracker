using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DataModel;

public class Select2Result
{
    public Select2Result(List<string> list)
    {
        foreach (var s in list) Results.Add(new Select2ResultItem { Id = s, Text = s });
    }

    public Select2Result(Dictionary<int, string> dictionary)
    {
        foreach (var pair in dictionary)
            Results.Add(new Select2ResultItem { Id = pair.Key.ToString(), Text = pair.Value });
    }

    [JsonPropertyName("results")] public List<Select2ResultItem> Results { get; set; } = new();
}