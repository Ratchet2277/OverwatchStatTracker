using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using DomainModel.Types;

namespace DomainModel;

public class Map
{
    public int Id { get; set; }
    [StringLength(50)] public string Name { get; set; }
    public MapType Type { get; set; }
    [StringLength(255)] public string ImageUrl { get; set; }

    [JsonIgnore] public virtual Collection<Season> Seasons { get; set; }
}