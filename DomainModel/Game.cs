using System;
using System.Collections.ObjectModel;
using DomainModel.Types;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace DomainModel
{
    public class Game
    {
        private readonly ILazyLoader _lazyLoader;

        private Collection<Hero> _heroes;

        private Map _map;

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

        public Map Map
        {
            get => _lazyLoader.Load(this, ref _map);
            set => _map = value;
        }

        public Collection<Hero> Heroes
        {
            get => _lazyLoader.Load(this, ref _heroes);
            set => _heroes = value;
        }

        public int AllieScore { get; set; }
        public int EnemyScore { get; set; }
        public GameType Type { get; set; }

        public Collection<SquadMember> SquadsMembers
        {
            get => _lazyLoader.Load(this, ref _squadMembers);
            set => _squadMembers = value;
        }

        public User User
        {
            get => _lazyLoader.Load(this, ref _user);
            set => _user = value;
        }
    }
}