using System.Collections.ObjectModel;
using DomainModel.Types;

namespace DomainModel
{
    public class Hero
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Role Role { get; set; }
        public virtual Collection<Season> Seasons { get; set; } 
        public string ImageUrl { get; set; }
        public virtual Collection<Game> Games { get; set; }
    }
}