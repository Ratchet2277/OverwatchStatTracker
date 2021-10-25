using System.Collections.ObjectModel;
using System.Linq;
using DAL;
using DomainModel;
using DomainModel.Types;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Initializer;

public class TrackerContextExtension : TrackerContext
{
    private readonly IConfiguration _configuration;

    public TrackerContextExtension(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void Initialize(bool dropAlways = false)
    {
        if (dropAlways)
            Database.EnsureDeleted();

        Database.EnsureCreated();

        //if db has been already seeded
        if (Maps.Any() || Heroes.Any())
            return;

        #region AddHeroes

        Collection<Hero> heroes = new()
        {
            new Hero
            {
                Name = "Ana",
                Role = GameRole.Support,
                ImageUrl = "/assets/heroes/ana.png"
            },
            new Hero
            {
                Name = "Ashe",
                Role = GameRole.Damage,
                ImageUrl = "/assets/heroes/ashe.png"
            },
            new Hero
            {
                Name = "Baptiste",
                Role = GameRole.Support,
                ImageUrl = "/assets/heroes/baptiste.png"
            },
            new Hero
            {
                Name = "Bastion",
                Role = GameRole.Damage,
                ImageUrl = "/assets/heroes/bastion.png"
            },
            new Hero
            {
                Name = "Brigitte",
                Role = GameRole.Support,
                ImageUrl = "/assets/heroes/brigitte.png"
            },
            new Hero
            {
                Name = "D.Va",
                Role = GameRole.Tank,
                ImageUrl = "/assets/heroes/dva.png"
            },
            new Hero
            {
                Name = "Doomfist",
                Role = GameRole.Damage,
                ImageUrl = "/assets/heroes/doomfist.png"
            },
            new Hero
            {
                Name = "Echo",
                Role = GameRole.Damage,
                ImageUrl = "/assets/heroes/echo.png"
            },
            new Hero
            {
                Name = "Genji",
                Role = GameRole.Damage,
                ImageUrl = "/assets/heroes/genji.png"
            },
            new Hero
            {
                Name = "Hanzo",
                Role = GameRole.Damage,
                ImageUrl = "/assets/heroes/hanzo.png"
            },
            new Hero
            {
                Name = "Junkrat",
                Role = GameRole.Damage,
                ImageUrl = "/assets/heroes/junkrat.png"
            },
            new Hero
            {
                Name = "Lúcio",
                Role = GameRole.Support,
                ImageUrl = "/assets/heroes/lucio.png"
            },
            new Hero
            {
                Name = "McCree",
                Role = GameRole.Damage,
                ImageUrl = "/assets/heroes/mccree.png"
            },
            new Hero
            {
                Name = "Mei",
                Role = GameRole.Damage,
                ImageUrl = "/assets/heroes/mei.png"
            },
            new Hero
            {
                Name = "Mercy",
                Role = GameRole.Support,
                ImageUrl = "/assets/heroes/mercy.png"
            },
            new Hero
            {
                Name = "Moira",
                Role = GameRole.Support,
                ImageUrl = "/assets/heroes/moira.png"
            },
            new Hero
            {
                Name = "Orisa",
                Role = GameRole.Tank,
                ImageUrl = "/assets/heroes/orisa.png"
            },
            new Hero
            {
                Name = "Pharah",
                Role = GameRole.Damage,
                ImageUrl = "/assets/heroes/pharah.png"
            },
            new Hero
            {
                Name = "Reaper",
                Role = GameRole.Damage,
                ImageUrl = "/assets/heroes/reaper.png"
            },
            new Hero
            {
                Name = "Reinhardt",
                Role = GameRole.Tank,
                ImageUrl = "/assets/heroes/reinhardt.png"
            },
            new Hero
            {
                Name = "Roadhog",
                Role = GameRole.Tank,
                ImageUrl = "/assets/heroes/roadhog.png"
            },
            new Hero
            {
                Name = "Sigma",
                Role = GameRole.Tank,
                ImageUrl = "/assets/heroes/sigma.png"
            },
            new Hero
            {
                Name = "Soldier: 76",
                Role = GameRole.Damage,
                ImageUrl = "/assets/heroes/soldier-76.png"
            },
            new Hero
            {
                Name = "Sombra",
                Role = GameRole.Damage,
                ImageUrl = "/assets/heroes/sombra.png"
            },
            new Hero
            {
                Name = "Symmetra",
                Role = GameRole.Damage,
                ImageUrl = "/assets/heroes/symmetra.png"
            },
            new Hero
            {
                Name = "Torbjörn",
                Role = GameRole.Damage,
                ImageUrl = "/assets/heroes/torbjorn.png"
            },
            new Hero
            {
                Name = "Tracer",
                Role = GameRole.Damage,
                ImageUrl = "/assets/heroes/tracer.png"
            },
            new Hero
            {
                Name = "Widowmaker",
                Role = GameRole.Damage,
                ImageUrl = "/assets/heroes/widowmaker.png"
            },
            new Hero
            {
                Name = "Winston",
                Role = GameRole.Tank,
                ImageUrl = "/assets/heroes/winston.png"
            },
            new Hero
            {
                Name = "Wrecking Ball",
                Role = GameRole.Tank,
                ImageUrl = "/assets/heroes/wrecking-ball.png"
            },
            new Hero
            {
                Name = "Zarya",
                Role = GameRole.Tank,
                ImageUrl = "/assets/heroes/zarya.png"
            },
            new Hero
            {
                Name = "Zenyatta",
                Role = GameRole.Support,
                ImageUrl = "/assets/heroes/zenyatta.png"
            }
        };

        #endregion

        #region AddMaps

        Collection<Map> maps = new()
        {
            new Map
            {
                Name = "Blizzard World",
                Type = MapType.Hybrid
            },
            new Map
            {
                Name = "Busan",
                Type = MapType.Control
            },
            new Map
            {
                Name = "Dorado",
                Type = MapType.Payload
            },
            new Map
            {
                Name = "Eichenwalde",
                Type = MapType.Hybrid
            },
            new Map
            {
                Name = "Hanamura",
                Type = MapType.Assault
            },
            new Map
            {
                Name = "Havana",
                Type = MapType.Payload
            },
            new Map
            {
                Name = "Hollywood",
                Type = MapType.Hybrid
            },
            new Map
            {
                Name = "Horizon Lunar Colony",
                Type = MapType.Assault
            },
            new Map
            {
                Name = "Ilios",
                Type = MapType.Control
            },
            new Map
            {
                Name = "Junkertown",
                Type = MapType.Payload
            },
            new Map
            {
                Name = "King's Row",
                Type = MapType.Hybrid
            },
            new Map
            {
                Name = "Lijiang Tower",
                Type = MapType.Control
            },
            new Map
            {
                Name = "Nepal",
                Type = MapType.Control
            },
            new Map
            {
                Name = "Numbani",
                Type = MapType.Hybrid
            },
            new Map
            {
                Name = "Oasis",
                Type = MapType.Control
            },
            new Map
            {
                Name = "Paris",
                Type = MapType.Assault
            },
            new Map
            {
                Name = "Rialto",
                Type = MapType.Payload
            },
            new Map
            {
                Name = "Route 66",
                Type = MapType.Payload
            },
            new Map
            {
                Name = "Temple of Anubis",
                Type = MapType.Assault
            },
            new Map
            {
                Name = "Volskaya Industries",
                Type = MapType.Assault
            },
            new Map
            {
                Name = "Watchpoint: Gibraltar",
                Type = MapType.Payload
            }
        };

        #endregion

        #region CreateSeason

        Season s29 = new()
        {
            Number = 29,
            HeroPool = heroes,
            MapPool = new Collection<Map>(maps.Where(m => m.Name is not "Paris" or "Horizon Lunar Colony").ToList())
        };

        #endregion

        Heroes.AddRange(heroes);
        Maps.AddRange(maps);
        Seasons.Add(s29);

        SaveChanges();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            _configuration.GetConnectionString("TrackerDB"));
        base.OnConfiguring(optionsBuilder);
    }
}