using System.Collections.ObjectModel;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace DomainModel
{
    public class User
    {
        private Collection<Game> _games;
        private readonly ILazyLoader _lazyLoader;

        public User()
        {
        }

        public User(ILazyLoader lazyLoader)
        {
            _lazyLoader = lazyLoader;
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public Collection<Game> Games
        {
            get => _lazyLoader.Load(this, ref _games);
            set => _games = value;
        }
    }
}