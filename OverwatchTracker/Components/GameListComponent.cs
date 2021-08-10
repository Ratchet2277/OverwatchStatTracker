using System;
using System.Threading.Tasks;
using Business.Contracts;
using DataModel;
using DomainModel.Types;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace WebApplication.Components
{
    public class GameListComponent : BaseComponent
    {
        private readonly IGameBusiness _gamesBusiness;

        public GameListComponent(IGameBusiness gamesBusiness,
            IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _gamesBusiness = gamesBusiness;
        }

        public async Task<IViewComponentResult> InvokeAsync(int page = 1, int size = 10, GameType? type = null)
        {
            return View(ActivatorUtilities.CreateInstance<GameListModel>(ServiceProvider,
                await _gamesBusiness.GetGames(page, size, type)));
        }
    }
}