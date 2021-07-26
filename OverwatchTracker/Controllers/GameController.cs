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

        public GameController(TrackerContext context, ILogger<GameController> logger, UserManager<User> userManager,
            SeasonBusiness seasonBusiness, GamesBusiness gamesBusiness) : base(context, logger, userManager)
        {
            _seasonBusiness = seasonBusiness;
            _gamesBusiness = gamesBusiness;
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

        [HttpGet("History\\{.type}\\{.page:int}")]
        public async Task<IActionResult> History(GameType? type, int page = 0)
        {
            var currentUser = await UserManager.GetUserAsync(User);

            var pagination =
                new Pagination<Game>(_seasonBusiness.GetLastSeason().Games.Where(g => g.User == currentUser));

            return View(new GameHistoryModel(pagination));
        }
    }
}