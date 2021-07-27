using System;
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
using WebApplication.Business;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    [Authorize]
    [Route("Game")]
    public partial class GameController : BaseController
    {
        private readonly SeasonBusiness _seasonBusiness;
        private readonly GamesBusiness _gamesBusiness;
        private readonly SrEvolution _srEvolution;

        public GameController(TrackerContext context, ILogger<GameController> logger, UserManager<User> userManager,
            SeasonBusiness seasonBusiness, GamesBusiness gamesBusiness, SrEvolution srEvolution) : base(context, logger, userManager)
        {
            _seasonBusiness = seasonBusiness;
            _gamesBusiness = gamesBusiness;
            _srEvolution = srEvolution;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<RedirectToActionResult> Create(Game game)
        {
            game.DateTime = DateTime.Now;
            game.Map = await Context.Maps.FindAsync(game.NewMap);
            game.Heroes =
                new Collection<Hero>(await Context.Heroes.Where(h => game.NewHeroes.Contains(h.Id)).ToListAsync());
            game.Season = _seasonBusiness.GetLastSeason();
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

        [HttpGet("{.type?}")]
        [HttpGet("{.type?}/Page/{page:int?}")]
        public async Task<IActionResult> Index(int page = 0, GameType? type = null)
        {
            var currentUser = await UserManager.GetUserAsync(User);

            var pagination =
                new Pagination<Game>(_seasonBusiness.GetLastSeason().Games.Where(g => g.User == currentUser));

            return View(new GameHistoryModel(pagination));
        }

        [HttpGet("Edit/{id:int}")]
        public async Task<IActionResult> Edit(int id)
        {
            var game = await Context.Games.FirstOrDefaultAsync(g => g.Id == id);
            return View(game);
        }

        public async Task<IActionResult> Edit(Game game, string[] newSquadMembers, int newMap,
            int[] newHeroes)
        {
            game.Map = await Context.Maps.FindAsync(newMap);
            game.Heroes = new Collection<Hero>(await Context.Heroes.Where(h => newHeroes.Contains(h.Id)).ToListAsync());

            game.SquadMembers.Clear();

            if (newSquadMembers.Length > 0)
            {
                game.SquadMembers = new Collection<SquadMember>();
                foreach (var squadMember in newSquadMembers)
                {
                    if (Context.SquadMembers.Any(s => s.Name == squadMember))
                    {
                        game.SquadMembers.Add(Context.SquadMembers.First(s => s.Name == squadMember));
                        continue;
                    }

                    game.SquadMembers.Add(new SquadMember {Name = squadMember});
                }
            }

            Context.Games.Update(game);

            await Context.SaveChangesAsync();

            return RedirectToAction("Index", "Home");
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