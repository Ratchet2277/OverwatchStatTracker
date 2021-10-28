using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using DomainModel.Types;

namespace DomainModel
{
    public class SquadMember
    {
        [Required] public int Id { get; set; }

        [Required] public string Name { get; set; }
        public GameRole Role { get; set; }

        [Required] public virtual User MainUser { get; set; }

        [JsonIgnore] public virtual Collection<Game> Games { get; set; }
    }
}