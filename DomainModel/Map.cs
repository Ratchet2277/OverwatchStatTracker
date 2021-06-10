using System.Collections.ObjectModel;
using DomainModel.Types;

namespace DomainModel
{
    public class Map
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public MapType Type { get; set; }
        
        public virtual Collection<Season> Seasons { get; set; }
    }
}