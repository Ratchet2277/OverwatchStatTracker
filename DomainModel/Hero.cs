using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using DomainModel.Types;

namespace DomainModel
{
    public class Hero
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Role Role { get; set; }
        public string ImageUrl { get; set; }

        [JsonIgnore] public virtual Collection<Season> Seasons { get; set; }

        [JsonIgnore] public virtual Collection<Game> Games { get; set; }
    }
}