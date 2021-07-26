using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using DomainModel.Types;

namespace DomainModel
{
    public class Game
    {
        public int Id { get; set; }

        [Range(0, 5000)] [Required] public int Sr { get; set; }
        public DateTime DateTime { get; set; }

        [JsonIgnore] public virtual Collection<Hero> Heroes { get; set; }

        [JsonIgnore] public virtual Map Map { get; set; }

        [Range(0, int.MaxValue)] [Required] public int AllieScore { get; set; }

        [Range(0, int.MaxValue)] [Required] public int EnemyScore { get; set; }
        public GameType Type { get; set; }

        [JsonIgnore] public virtual Collection<SquadMember> SquadMembers { get; set; } = new();

        [JsonIgnore] public virtual User User { get; set; }

        [JsonIgnore] public virtual Season Season { get; set; }

        [NotMapped] public string[] NewSquadMembers { get; set; } = Array.Empty<string>();
        [NotMapped] public int[] NewHeroes { get; set; } = Array.Empty<int>();
        [NotMapped] public int NewMap { get; set; }
    }
}