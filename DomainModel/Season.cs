using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace DomainModel
{
    public class Season
    {
        private readonly ILazyLoader _lazyLoader;

        private Collection<Hero> _heroPool;

        private Collection<Map> _mapPool;

        public Season()
        {
        }

        public Season(ILazyLoader lazyLoader)
        {
            _lazyLoader = lazyLoader;
        }

        public int Id { get; set; }
        public int Number { get; set; }

        [JsonIgnore]
        public Collection<Hero> HeroPool
        {
            get => _lazyLoader.Load(this, ref _heroPool);
            set => _heroPool = value;
        }

        [JsonIgnore]
        public Collection<Map> MapPool
        {
            get => _lazyLoader.Load(this, ref _mapPool);
            set => _mapPool = value;
        }

        private Collection<Game> _games;

        [JsonIgnore]
        public Collection<Game> Games
        {
            get => _lazyLoader.Load(this, ref _games);
            set => _games = value;
        }
    }
}