using System;
using System.Threading.Tasks;
using DAL;
using DomainModel.Types;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using WebApplication.Business;
using WebApplication.Models;

namespace WebApplication.Components
{
    public class GameListComponent : BaseComponent
    {
        private readonly GamesBusiness _gamesBusiness;

        public GameListComponent(TrackerContext context, GamesBusiness gamesBusiness,
            IServiceProvider serviceProvider) : base(context, serviceProvider)
        {
            _gamesBusiness = gamesBusiness;
        }

        public async Task<IViewComponentResult> InvokeAsync(int page = 0, int size = 10, GameType? type = null)
        {
            return View(new GameListModel
            {
                Games = await _gamesBusiness.GetGames(page, size, type),
                SrEvolution = ServiceProvider.GetService<SrEvolution>()
            });
        }
    }
}