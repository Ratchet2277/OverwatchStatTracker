using System;
using System.Threading.Tasks;
using Business.Contracts;
using DataModel;
using DomainModel;
using DomainModel.Types;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Tracker.Controllers
{
    [Authorize]
    [Route("Game/")]
    public partial class GameController : BaseController
    {
        private readonly IGameBusiness _business;
        private readonly ISeasonBusiness _seasonBusiness;

        public GameController(ILogger<GameController> logger, UserManager<User> userManager,
            ISeasonBusiness seasonBusiness, IGameBusiness business, IServiceProvider serviceProvider) : base(
            logger, userManager, serviceProvider)
        {
            _seasonBusiness = seasonBusiness;
            _business = business;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<RedirectToActionResult> Create(Game game)
        {
            game.User = await UserManager.GetUserAsync(User);
            game.Season = await _seasonBusiness.GetLastSeason();

            await _business.Add(game);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet("History/{type?}")]
        [HttpGet("History/{type?}/Page/{page:int?}")]
        [HttpGet("History/Page/{page:int?}")]
        public async Task<IActionResult> History(int page = 1, GameType? type = null)
        {
            return View(ActivatorUtilities.CreateInstance<GameHistoryModel>(ServiceProvider,
                await _business.GetGames(page, 10, type)));
        }

        [HttpGet("Edit/{id:int}")]
        public async Task<IActionResult> Edit(int id)
        {
            var game = await _business.Get(id);
            var currentUser = await UserManager.GetUserAsync(User);
            if (game.User != currentUser) return Forbid();
            return View(game);
        }

        [HttpPost("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveEdit(Game newGame)
        {
            var game = await _business.Get(newGame.Id);
            var currentUser = await UserManager.GetUserAsync(User);
            if (game.User != currentUser) return Forbid();

            await _business.Update(newGame);

            return RedirectToAction("History");
        }
    }
}