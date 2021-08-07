#nullable enable
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WebApplication.Models
{
    public class DataSet<T>
    {
        private bool _isStepped;
        private string? _stepValue;

        public DataSet(string label)
        {
            Label = label;
        }

        [JsonPropertyName("label")] public string Label { get; }

        [JsonPropertyName("backgroundColor")] public List<string> BackgroundColor { get; set; } = new();

        [JsonPropertyName("data")] public List<T> Data { get; set; } = new();

        [JsonPropertyName("borderColor")] public string? BorderColor { get; set; }

        [JsonPropertyName("stepped")]
        public dynamic? Stepped
        {
            get => _stepValue is null ? _isStepped : _stepValue;
            set
            {
                switch (value)
                {
                    case string:
                        _stepValue = value;
                        _isStepped = false;
                        break;
                    case bool:
                        _isStepped = value;
                        _stepValue = null;
                        break;
                }
            }
        }

        public DataSet<T> AddData(T data)
        {
            Data.Add(data);
            return this;
        }

        public DataSet<T> AddData(IEnumerable<T> data)
        {
            Data.AddRange(data);
            return this;
        }

        public DataSet<T> AddBacgroundColor(string data)
        {
            BackgroundColor.Add(data);
            return this;
        }

        public DataSet<T> AddBacgroundColor(IEnumerable<string> data)
        {
            BackgroundColor.AddRange(data);
            return this;
        }
    }
}