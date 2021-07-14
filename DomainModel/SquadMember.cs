using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using DomainModel.Types;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace DomainModel
{
    public class SquadMember
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Role Role { get; set; }

        [JsonIgnore] public virtual Collection<Game> Games { get; set; }
    }
}