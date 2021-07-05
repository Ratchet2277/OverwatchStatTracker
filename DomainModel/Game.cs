using System;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using DomainModel.Types;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace DomainModel
{
    public class Game
    {
        private readonly ILazyLoader _lazyLoader;

        private Collection<Hero> _heroes;

        private Map _map;
        public Map Map
        {
            get => _lazyLoader.Load(this, ref _map);
            set => _map = value;
        }
        private Collection<SquadMember> _squadMembers;

        private User _user;

        public Game()
        {
        }

        public Game(ILazyLoader lazyLoader)
        {
            _lazyLoader = lazyLoader;
        }

        public int Id { get; set; }
        public int Sr { get; set; }
        public DateTime DateTime { get; set; }
        

        [JsonIgnore]
        public Collection<Hero> Heroes
        {
            get => _lazyLoader.Load(this, ref _heroes);
            set => _heroes = value;
        }

        public int AllieScore { get; set; }
        public int EnemyScore { get; set; }
        public GameType Type { get; set; }

        [JsonIgnore]
        public Collection<SquadMember> SquadMembers
        {
            get => _lazyLoader.Load(this, ref _squadMembers);
            set => _squadMembers = value;
        }
        
        [JsonIgnore]
        public User User
        {
            get => _lazyLoader.Load(this, ref _user);
            set => _user = value;
        }

        private Season _season;
        
        [JsonIgnore]
        public Season Season
        {
            get => _lazyLoader.Load(this, ref _season);
            set => _season = value;
        }
    }
}