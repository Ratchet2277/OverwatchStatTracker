using System;
using System.Threading.Tasks;
using Business;
using DataModel;
using DomainModel;
using DomainModel.Types;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Repository.Contracts;

namespace WebApplication.Controllers
{
    [Authorize]
    [Route("Game/")]
    public partial class GameController : BaseController
    {
        private readonly GamesBusiness _gamesBusiness;
        private readonly IGameRepository _repository;
        private readonly SeasonBusiness _seasonBusiness;

        public GameController(ILogger<GameController> logger, UserManager<User> userManager,
            SeasonBusiness seasonBusiness, GamesBusiness gamesBusiness, IServiceProvider serviceProvider,
            IGameRepository repository) : base(
            logger, userManager, serviceProvider)
        {
            _seasonBusiness = seasonBusiness;
            _gamesBusiness = gamesBusiness;
            _repository = repository;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<RedirectToActionResult> Create(Game game)
        {
            game.User = await UserManager.GetUserAsync(User);
            game.Season = _seasonBusiness.GetLastSeason();

            await _repository.Add(game);

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
            var game = await _repository.Get(id);
            var currentUser = await UserManager.GetUserAsync(User);
            if (game.User != currentUser) return Forbid();
            return View(game);
        }

        [HttpPost("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveEdit(Game newGame)
        {
            var game = await _repository.Get(newGame.Id);
            var currentUser = await UserManager.GetUserAsync(User);
            if (game.User != currentUser) return Forbid();

            await _repository.Update(newGame);

            return RedirectToAction("History");
        }
    }
}