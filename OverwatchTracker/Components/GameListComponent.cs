using System;
using System.Threading.Tasks;
using DAL;
using DomainModel.Types;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using WebApplication.Buisness;
using WebApplication.Models;

namespace WebApplication.Components
{
    public class GameListComponent : BaseComponent
    {
        private readonly GamesBuisness _gamesBuisness;

        public GameListComponent(TrackerContext context, GamesBuisness gamesBuisness,
            IServiceProvider serviceProvider) : base(context, serviceProvider)
        {
            _gamesBuisness = gamesBuisness;
        }

        public async Task<IViewComponentResult> InvokeAsync(int page = 0, int size = 10, GameType? type = null)
        {
            return View(new GameListModel
            {
                Games = await _gamesBuisness.GetGames(page, size, type),
                SrEvolution = ServiceProvider.GetService<SrEvolution>()
            });
        }
    }
}