using System;
using System.Collections.ObjectModel;
using DomainModel.Types;

namespace DomainModel
{
    public class Game
    {
        public int Id { get; set; }
        public int Sr { get; set; }
        public DateTime DateTime { get; set; }
        public virtual Map Map { get; set; }
        public virtual Collection<Hero> Heroes { get; set; }
        public int AllieScore { get; set; }
        public int EnemyScore { get; set; }
        public GameType Type { get; set; }
        public virtual Collection<SquadMember> SquadsMembers { get; set; }
        public virtual User User { get; set; }
    }
}