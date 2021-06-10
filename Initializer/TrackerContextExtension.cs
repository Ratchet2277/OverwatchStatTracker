using System.Collections.Generic;
using System.Linq;
using DAL;
using DomainModel;
using DomainModel.Types;

namespace Initializer
{
    public static class TrackerContextExtension
    {
        public static void Initialize(this TrackerContext context, bool dropAlways = false)
        {
            if (dropAlways)
                context.Database.EnsureDeleted();

            context.Database.EnsureCreated();

            //if db has been already seeded
            if (context.Maps.Any() || context.Heroes.Any())
                return;


            #region AddHeroes

            List<Hero> heroes = new()
            {
                new Hero
                {
                    Name = "Ana",
                    Role = Role.Support,
                    ImageUrl = "/assets/heroes/ana.png"
                },
                new Hero
                {
                    Name = "Ashe",
                    Role = Role.Damage,
                    ImageUrl = "/assets/heroes/ashe.png"
                },
                new Hero
                {
                    Name = "Baptiste",
                    Role = Role.Support,
                    ImageUrl = "/assets/heroes/baptiste.png"
                },
                new Hero
                {
                    Name = "Bastion",
                    Role = Role.Damage,
                    ImageUrl = "/assets/heroes/bastion.png"
                },
                new Hero
                {
                    Name = "Brigitte",
                    Role = Role.Support,
                    ImageUrl = "/assets/heroes/brigitte.png"
                },
                new Hero
                {
                    Name = "D.Va",
                    Role = Role.Tank,
                    ImageUrl = "/assets/heroes/dva.png"
                },
                new Hero
                {
                    Name = "Doomfist",
                    Role = Role.Damage,
                    ImageUrl = "/assets/heroes/doomfist.png"
                },
                new Hero
                {
                    Name = "Echo",
                    Role = Role.Damage,
                    ImageUrl = "/assets/heroes/echo.png"
                },
                new Hero
                {
                    Name = "Genji",
                    Role = Role.Damage,
                    ImageUrl = "/assets/heroes/genji.png"
                },
                new Hero
                {
                    Name = "Hanzo",
                    Role = Role.Damage,
                    ImageUrl = "/assets/heroes/hanzo.png"
                },
                new Hero
                {
                    Name = "Junkrat",
                    Role = Role.Damage,
                    ImageUrl = "/assets/heroes/junkrat.png"
                },
                new Hero
                {
                    Name = "Lúcio",
                    Role = Role.Support,
                    ImageUrl = "/assets/heroes/lucio.png"
                },
                new Hero
                {
                    Name = "McCree",
                    Role = Role.Damage,
                    ImageUrl = "/assets/heroes/mccree.png"
                },
                new Hero
                {
                    Name = "Mei",
                    Role = Role.Damage,
                    ImageUrl = "/assets/heroes/mei.png"
                },
                new Hero
                {
                    Name = "Mercy",
                    Role = Role.Support,
                    ImageUrl = "/assets/heroes/mercy.png"
                },
                new Hero
                {
                    Name = "Moira",
                    Role = Role.Support,
                    ImageUrl = "/assets/heroes/support.png"
                },
                new Hero
                {
                    Name = "Orisa",
                    Role = Role.Tank,
                    ImageUrl = "/assets/heroes/orisa.png"
                },
                new Hero
                {
                    Name = "Pharah",
                    Role = Role.Damage,
                    ImageUrl = "/assets/heroes/pharah.png"
                },
                new Hero
                {
                    Name = "Reaper",
                    Role = Role.Damage,
                    ImageUrl = "/assets/heroes/reaper.png"
                },
                new Hero
                {
                    Name = "Reinhardt",
                    Role = Role.Tank,
                    ImageUrl = "/assets/heroes/reinhardt.png"
                },
                new Hero
                {
                    Name = "Roadhog",
                    Role = Role.Tank,
                    ImageUrl = "/assets/heroes/roadhog.png"
                },
                new Hero
                {
                    Name = "Sigma",
                    Role = Role.Tank,
                    ImageUrl = "/assets/heroes/sigma.png"
                },
                new Hero
                {
                    Name = "Soldier: 76",
                    Role = Role.Damage,
                    ImageUrl = "/assets/heroes/soldier-76.png"
                },
                new Hero
                {
                    Name = "Sombra",
                    Role = Role.Damage,
                    ImageUrl = "/assets/heroes/sombra.png"
                },
                new Hero
                {
                    Name = "Symmetra",
                    Role = Role.Damage,
                    ImageUrl = "/assets/heroes/symmetra.png"
                },
                new Hero
                {
                    Name = "Torbjörn",
                    Role = Role.Damage,
                    ImageUrl = "/assets/heroes/torbjorn.png"
                },
                new Hero
                {
                    Name = "Tracer",
                    Role = Role.Damage,
                    ImageUrl = "/assets/heroes/tracer.png"
                },
                new Hero
                {
                    Name = "Widowmaker",
                    Role = Role.Damage,
                    ImageUrl = "/assets/heroes/widowmaker.png"
                },
                new Hero
                {
                    Name = "Winston",
                    Role = Role.Tank,
                    ImageUrl = "/assets/heroes/winston.png"
                },
                new Hero
                {
                    Name = "Wrecking Ball",
                    Role = Role.Tank,
                    ImageUrl = "/assets/heroes/wrecking-ball.png"
                },
                new Hero
                {
                    Name = "Zarya",
                    Role = Role.Tank,
                    ImageUrl = "/assets/heroes/zarya.png"
                },
                new Hero
                {
                    Name = "Zenyatta",
                    Role = Role.Support,
                    ImageUrl = "/assets/heroes/zenyatta.png"
                }
            };

            #endregion

            #region AddMaps

            List<Map> maps = new()
            {
                new Map()
                {
                    Name = "Blizzard World",
                    Type = MapType.Hybrid
                },
                new Map()
                {
                    Name = "Busan",
                    Type = MapType.Control
                },
                new Map()
                {
                    Name = "Dorado",
                    Type = MapType.Payload
                },
                new Map()
                {
                    Name = "Eichenwalde",
                    Type = MapType.Hybrid
                },
                new Map()
                {
                    Name = "Hanamura",
                    Type = MapType.Assault
                },
                new Map()
                {
                    Name = "Havana",
                    Type = MapType.Payload
                },
                new Map()
                {
                    Name = "Hollywood",
                    Type = MapType.Hybrid
                },
                new Map()
                {
                    Name = "Horizon Lunar Colony",
                    Type = MapType.Assault
                },
                new Map()
                {
                    Name = "Ilios",
                    Type = MapType.Control
                },
                new Map()
                {
                    Name = "Junkertown",
                    Type = MapType.Payload
                },
                new Map()
                {
                    Name = "King's Row",
                    Type = MapType.Hybrid
                },
                new Map()
                {
                    Name = "Lijiang Tower",
                    Type = MapType.Control
                },
                new Map()
                {
                    Name = "Nepal",
                    Type = MapType.Control
                },
                new Map()
                {
                    Name = "Numbani",
                    Type = MapType.Hybrid
                },
                new Map()
                {
                    Name = "Oasis",
                    Type = MapType.Control
                },
                new Map()
                {
                    Name = "Paris",
                    Type = MapType.Assault
                },
                new Map()
                {
                    Name = "Rialto",
                    Type = MapType.Payload
                },
                new Map()
                {
                    Name = "Route 66",
                    Type = MapType.Payload
                },
                new Map()
                {
                    Name = "Temple of Anubis",
                    Type = MapType.Assault
                },
                new Map()
                {
                    Name = "Volskaya Industries",
                    Type = MapType.Assault
                },
                new Map()
                {
                    Name = "Watchpoint: Gibraltar",
                    Type = MapType.Payload
                },
            };

            #endregion
            
            context.Heroes.AddRange(heroes);
            context.Maps.AddRange(maps);
            context.SaveChanges();
        }
    }
}