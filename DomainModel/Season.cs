using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace DomainModel
{
    public class Season
    {
        public int Id { get; set; }
        public int Number { get; set; }

        [JsonIgnore] public virtual Collection<Hero> HeroPool { get; set; }

        [JsonIgnore] public virtual Collection<Map> MapPool { get; set; }

        [JsonIgnore] public virtual Collection<Game> Games { get; set; }
    }
}