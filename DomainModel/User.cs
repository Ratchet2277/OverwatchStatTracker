using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace DomainModel
{
    public class User : IdentityUser
    {
        private readonly ILazyLoader _lazyLoader;
        private Collection<Game> _games;

        public User()
        {
        }

        public User(ILazyLoader lazyLoader)
        {
            _lazyLoader = lazyLoader;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        [JsonIgnore]
        public Collection<Game> Games
        {
            get => _lazyLoader.Load(this, ref _games);
            set => _games = value;
        }
    }
}