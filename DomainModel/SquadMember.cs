using System.Collections.ObjectModel;
using DomainModel.Types;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace DomainModel
{
    public class SquadMember
    {
        private Collection<Game> _games;
        private readonly ILazyLoader _lazyLoader;

        public SquadMember()
        {
        }

        public SquadMember(ILazyLoader lazyLoader)
        {
            _lazyLoader = lazyLoader;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public Role Role { get; set; }

        public Collection<Game> Games
        {
            get => _lazyLoader.Load(this, ref _games);
            set => _games = value;
        }
    }
}