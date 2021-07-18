using System;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using DomainModel.Types;

namespace DomainModel
{
    public class Game
    {
        public int Id { get; set; }
        public int Sr { get; set; }
        public DateTime DateTime { get; set; }

        [JsonIgnore] public virtual Collection<Hero> Heroes { get; set; }

        [JsonIgnore] public virtual Map Map { get; set; }
        public int AllieScore { get; set; }
        public int EnemyScore { get; set; }
        public GameType Type { get; set; }

        [JsonIgnore] public virtual Collection<SquadMember> SquadMembers { get; set; }

        [JsonIgnore] public virtual User User { get; set; }

        [JsonIgnore] public virtual Season Season { get; set; }
    }
}