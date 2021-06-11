using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using DomainModel.Types;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace DomainModel
{
    public class Map
    {
        private readonly ILazyLoader _lazyLoader;

        private Collection<Season> _seasons;

        public Map()
        {
        }

        public Map(ILazyLoader lazyLoader)
        {
            _lazyLoader = lazyLoader;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public MapType Type { get; set; }
        public string ImageUrl { get; set; }

        [JsonIgnore]
        public virtual Collection<Season> Seasons
        {
            get => _lazyLoader.Load(this, ref _seasons);
            set => _seasons = value;
        }
    }
}