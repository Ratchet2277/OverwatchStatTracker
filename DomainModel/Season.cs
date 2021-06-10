using System.Collections.ObjectModel;

namespace DomainModel
{
    public class Season
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public virtual Collection<Hero> HeroPool { get; set; }
        public virtual Collection<Map> MapPool { get; set; }
    }
}