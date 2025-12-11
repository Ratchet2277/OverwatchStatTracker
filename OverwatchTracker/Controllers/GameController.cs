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

namespace WebApplication.Controllers;

[Authorize]
[Route("Game/")]
public partial class GameController(
    ILogger<GameController> logger,
    UserManager<User> userManager,
    ISeasonBusiness seasonBusiness,
    IGameBusiness business,
    IServiceProvider serviceProvider)
    : BaseController(logger, userManager)
{
    private const string RedirectOnError = "Index";
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<RedirectToActionResult> Create(Game game)
    {
        if (!ModelState.IsValid)
        {
            return RedirectToAction(RedirectOnError, "Home");
        }


        game.User = await UserManager.GetUserAsync(User);
        game.Season = await seasonBusiness.GetLastSeason();

        await business.Add(game);

        return RedirectToAction(RedirectOnError, "Home");
    }

    [HttpGet("History/{type?}")]
    [HttpGet("History/{type?}/Page/{page:int?}")]
    [HttpGet("History/Page/{page:int?}")]
    public async Task<IActionResult> History(int page = 1, GameType? type = null)
    {
        if (!ModelState.IsValid)
            RedirectToAction(RedirectOnError, "Home");
        
        return View(ActivatorUtilities.CreateInstance<GameHistoryModel>(serviceProvider,
            await business.GetGames(page, 10, type)));
    }

    [HttpGet("Edit/{id:int}")]
    public async Task<IActionResult> Edit(int id)
    {
        if (!ModelState.IsValid)
            RedirectToAction(RedirectOnError, "Home");
        
        var game = await business.Get(id);
        var currentUser = await UserManager.GetUserAsync(User);
        if (game?.User != currentUser) return Forbid();
        return View(game);
    }

    [HttpPost("Edit")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SaveEdit(Game newGame)
    {
        if (!ModelState.IsValid)
            RedirectToAction("Index", "Home");
        
        var game = await business.Get(newGame.Id);
        var currentUser = await UserManager.GetUserAsync(User);
        if (game != null && game.User != currentUser) return Forbid();

        await business.Update(newGame);

        return RedirectToAction("History");
    }
}