﻿#nullable enable
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WebApplication.Models
{
    public class DataSet<T>
    {
        [JsonPropertyName("label")]
        public string Label { get; }

        [JsonPropertyName("backgroundColor")]
        public List<string> BackgroundColor { get; set; } = new();

        [JsonPropertyName("data")] 
        public List<T> Data { get; set; } = new();
        
        [JsonPropertyName("borderColor")]
        public string? BorderColor { get; set; }

        private bool _isSteped;
        private string? _stepValue;

        [JsonPropertyName("stepped")]
        public dynamic? Stepped
        {
            get => _stepValue is null ? _isSteped : _stepValue;
            set
            {
                switch (value)
                {
                    case string:
                        _stepValue = value;
                        _isSteped = false;
                        break;
                    case bool:
                        _isSteped = value;
                        _stepValue = null;
                        break;
                }
            }
        }
        
        public DataSet(string label)
        {
            Label = label;
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