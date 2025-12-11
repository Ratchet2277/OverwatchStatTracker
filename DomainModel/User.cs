using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;

namespace DomainModel;

public class User : IdentityUser
{
    [StringLength(50)] public string FirstName { get; set; }
    [StringLength(50)] public string LastName { get; set; }

    [JsonIgnore] public virtual Collection<Game> Games { get; set; }
}