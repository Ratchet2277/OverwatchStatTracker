using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using DAL;
using DomainModel;
using DomainModel.Types;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebApplication.Buisness;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    [Authorize]
    [Route("Game")]
    public class GameController : BaseController
    {
        private readonly SeasonBuisness _seasonBuisness;

        public GameController(TrackerContext context, ILogger<GameController> logger, UserManager<User> userManager,
            SeasonBuisness seasonBuisness) : base(context, logger, userManager)
        {
            _seasonBuisness = seasonBuisness;
        }

        [ResponseCache(Duration = 3600)]
        [HttpGet("MapList")]
        public List<Map> MapList()
        {
            return _seasonBuisness.GetLastSeason().MapPool.OrderBy(m => m.Name).ToList();
        }

        [HttpGet("RoleList")]
        [ResponseCache(Duration = 3600)]
        public Dictionary<int, string> RoleList()
        {
            return Enum.GetValues(typeof(GameType)).Cast<GameType>().ToDictionary(t => (int) t, t => t.ToString());
        }

        [HttpGet("HeroList/{roleId:int?}")]
        [ResponseCache(Duration = 3600, VaryByHeader = "roleId")]
        public async Task<List<Hero>> HeroList(int? roleId)
        {
            var season = await Context.Seasons.OrderByDescending(s => s.Number).FirstAsync();
            IEnumerable<Hero> query = season.HeroPool.OrderBy(h => h.Name);

            if (Enum.TryParse(roleId.ToString(), out Role role) && season.HeroPool.Any(h => h.Role == role))
                query = query.Where(h => h.Role == role);

            return query.ToList();
        }

        [HttpPost("/Create")]
        [ValidateAntiForgeryToken]
        public async Task<RedirectToActionResult> Create(Game game)
        {
            game.DateTime = DateTime.Now;
            game.Map = await Context.Maps.FindAsync(game.NewMap);
            game.Heroes =
                new Collection<Hero>(await Context.Heroes.Where(h => game.NewHeroes.Contains(h.Id)).ToListAsync());
            game.Season = _seasonBuisness.GetLastSeason();
            game.User = await UserManager.GetUserAsync(User);

            if (game.NewSquadMembers.Length > 0)
            {
                game.SquadMembers = new Collection<SquadMember>();
                foreach (var squadMember in game.NewSquadMembers)
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

        [HttpGet("/Edit/{id:int}")]
        public async Task<IActionResult> Edit(int id)
        {
            var game = await Context.Games.FirstAsync(g => g.Id == id);

            return View(game);
        }

        [HttpPost("/Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Game game)
        {
            game.Map = await Context.Maps.FindAsync(game.NewMap);
            game.Heroes =
                new Collection<Hero>(await Context.Heroes.Where(h => game.NewHeroes.Contains(h.Id)).ToListAsync());

            if (game.NewSquadMembers.Length > 0)
            {
                game.SquadMembers = new Collection<SquadMember>();
                foreach (var squadMemberName in game.NewSquadMembers)
                {
                    if (Context.SquadMembers.Any(s => s.Name == squadMemberName))
                    {
                        game.SquadMembers.Add(Context.SquadMembers.First(s => s.Name == squadMemberName));
                        continue;
                    }

                    game.SquadMembers.Add(new SquadMember {Name = squadMemberName});
                }
            }

            Context.Games.Update(game);

            await Context.SaveChangesAsync();

            return RedirectToAction("Index", "Home");
        }
    }
}