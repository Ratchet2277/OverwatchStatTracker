using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using DomainModel.Types;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace DomainModel
{
    public class Hero
    {
        private Collection<Game> _games;
        private readonly ILazyLoader _lazyLoader;

        private Collection<Season> _seasons;

        public Hero()
        {
        }

        public Hero(ILazyLoader lazyLoader)
        {
            _lazyLoader = lazyLoader;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public Role Role { get; set; }
        public string ImageUrl { get; set; }
        
        [JsonIgnore]
        public Collection<Season> Seasons
        {
            get => _lazyLoader.Load(this, ref _seasons);
            set => _seasons = value;
        }

        [JsonIgnore]
        public Collection<Game> Games
        {
            get => _lazyLoader.Load(this, ref _games);
            set => _games = value;
        }
    }
}