using System.Collections.ObjectModel;

namespace DomainModel
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public virtual Collection<Game> Games { get; set; }
    }
}