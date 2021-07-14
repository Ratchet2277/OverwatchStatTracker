using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace DomainModel
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [JsonIgnore] public virtual Collection<Game> Games { get; set; }
    }
}