﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using DAL;
using DomainModel;
using DomainModel.Types;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace WebApplication.Controllers
{
    public class GameController : BaseController
    {
        public GameController(TrackerContext context, ILogger<GameController> logger) : base(context, logger)
        {
        }

        [ResponseCache(Duration = 3600)]
        [HttpGet]
        public async Task<List<Map>> MapList()
        {
            return (await Context.Seasons.OrderByDescending(s => s.Number).FirstAsync()).MapPool
                .Select(m => new Map {Name = m.Name, Id = m.Id, Type = m.Type, ImageUrl = m.ImageUrl}).ToList();
        }

        [HttpGet]
        [ResponseCache(Duration = 3600)]
        public Dictionary<int, string> RoleList()
        {
            return Enum.GetValues(typeof(GameType)).Cast<GameType>().ToDictionary(t => (int) t, t => t.ToString());
        }

        [HttpGet("Game/HeroList/{roleId:int?}")]
        [ResponseCache(Duration = 3600, VaryByHeader = "roleId")]
        public async Task<List<Hero>> HeroList(int? roleId)
        {
            var season = await Context.Seasons.OrderByDescending(s => s.Number).FirstAsync();
            IEnumerable<Hero> query = season.HeroPool.OrderBy(h => h.Name);

            if (Enum.TryParse(roleId.ToString(), out Role role) && season.HeroPool.Any(h => h.Role == role))
            {
                query = query.Where(h => h.Role == role);
            }

            return query.Select(m => new Hero {Name = m.Name, Id = m.Id, Role = m.Role, ImageUrl = m.ImageUrl})
                .ToList();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<RedirectToActionResult> Create(Game game, string[] newSquadsMembers, int newMap,
            int[] newHeroes)
        {
            game.DateTime = DateTime.Now;
            game.Map = await Context.Maps.FindAsync(newMap);
            game.Heroes = new Collection<Hero>(await Context.Heroes.Where(h => newHeroes.Contains(h.Id)).ToListAsync());

            if (newSquadsMembers.Length > 0)
            {
                game.SquadMembers = new Collection<SquadMember>();
                foreach (var squadMember in newSquadsMembers)
                {
                    if (Context.SquadMembers.Any(s => s.Name == squadMember))
                    {
                        game.SquadMembers.Add(Context.SquadMembers.First(s => s.Name == squadMember));
                        continue;
                    }

                    game.SquadMembers.Add(new SquadMember {Name = squadMember});
                }
            }

            Context.Games.Add(game);

            await Context.SaveChangesAsync();

            return RedirectToAction("Index", "Home");
        }
    }
}