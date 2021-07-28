using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using DAL;
using DomainModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebApplication.Buisness;

namespace WebApplication.Controllers
{
    [Authorize]
    [Route("Game")]
    public partial class GameController : BaseController
    {
        private readonly SeasonBuisness _seasonBuisness;

        public GameController(TrackerContext context, ILogger<GameController> logger, UserManager<User> userManager,
            SeasonBuisness seasonBuisness) : base(context, logger, userManager)
        {
            _seasonBuisness = seasonBuisness;
        }

        [HttpPost]
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
                    if (Context.SquadMembers.Any(s => s.Name == squadMember && s.MainUser == game.User))
                    {
                        game.SquadMembers.Add(Context.SquadMembers.First(s => s.Name == squadMember));
                        continue;
                    }

                    game.SquadMembers.Add(new SquadMember {Name = squadMember, MainUser = game.User});
                }
            }

            Context.Games.Add(game);

            await Context.SaveChangesAsync();

            return RedirectToAction("Index", "Home");
        }
    }
}