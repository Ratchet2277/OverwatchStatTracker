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
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WebApplication.Business;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    [Authorize]
    [Route("Game/")]
    public partial class GameController : BaseController
    {
        private readonly GamesBusiness _gamesBusiness;
        private readonly SeasonBusiness _seasonBusiness;

        public GameController(TrackerContext context, ILogger<GameController> logger, UserManager<User> userManager,
            SeasonBusiness seasonBusiness, GamesBusiness gamesBusiness, IServiceProvider serviceProvider) : base(
            context, logger, userManager, serviceProvider)
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
                    if (Context.SquadMembers.Any(s => s.Name == squadMember && s.MainUser == game.User))
                    {
                        game.SquadMembers.Add(Context.SquadMembers.First(s =>
                            s.Name == squadMember && s.MainUser == game.User));
                        continue;
                    }

                    game.SquadMembers.Add(new SquadMember {Name = squadMember, MainUser = game.User});
                }
            }

            Context.Games.Add(game);

            await Context.SaveChangesAsync();

            return RedirectToAction("Index", "Home");
        }

        [HttpGet("History/{type?}")]
        [HttpGet("History/{type?}/Page/{page:int?}")]
        [HttpGet("History/Page/{page:int?}")]
        public async Task<IActionResult> History(int page = 1, GameType? type = null)
        {
            return View(ActivatorUtilities.CreateInstance<GameHistoryModel>(ServiceProvider,
                await _gamesBusiness.GetGames(page, 10, type)));
        }

        [HttpGet("Edit/{id:int}")]
        public async Task<IActionResult> Edit(int id)
        {
            var game = await Context.Games.FirstOrDefaultAsync(g => g.Id == id);
            return View(game);
        }

        [HttpPost("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveEdit(Game newGame)
        {
            var game = await Context.Games.FindAsync(newGame.Id);
            game.AllieScore = newGame.AllieScore;
            game.EnemyScore = newGame.EnemyScore;
            game.Sr = newGame.Sr;

            if (game.Map.Id != newGame.NewMap) game.Map = await Context.Maps.FindAsync(newGame.NewMap);

            foreach (var heroToDel in game.Heroes.Where(h => !newGame.NewHeroes.Contains(h.Id)).ToList())
                game.Heroes.Remove(heroToDel);

            foreach (var heroToAdd in Context.Heroes.Where(h =>
                newGame.NewHeroes.Contains(h.Id) && !game.Heroes.Contains(h))) game.Heroes.Add(heroToAdd);

            foreach (var squadMemberName in newGame.NewSquadMembers)
            {
                if (game.SquadMembers.Any(s => newGame.NewSquadMembers.Contains(s.Name)))
                    continue;

                if (Context.SquadMembers.Any(s => s.Name == squadMemberName && s.MainUser == game.User))
                {
                    game.SquadMembers.Add(Context.SquadMembers.First(s => s.Name == squadMemberName));
                    continue;
                }

                game.SquadMembers.Add(new SquadMember {Name = squadMemberName, MainUser = game.User});
            }

            foreach (var squadMemberToDel in game.SquadMembers.Where(s => !newGame.NewSquadMembers.Contains(s.Name))
                .ToList())
                game.SquadMembers.Remove(squadMemberToDel);

            Context.Games.Update(game);

            await Context.SaveChangesAsync();

            return RedirectToAction("History");
        }
    }
}