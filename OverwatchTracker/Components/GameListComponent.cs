using System;
using System.Linq;
using System.Threading.Tasks;
using DAL;
using DomainModel;
using DomainModel.Types;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using WebApplication.Buisness;
using WebApplication.Models;

namespace WebApplication.Components
{
    public class GameListComponent : BaseComponent
    {
        private readonly SeasonBuisness _seasonBuisness;
        private readonly UserManager<User> _userManager;

        public GameListComponent(TrackerContext context, UserManager<User> userManager, SeasonBuisness seasonBuisness,
            IServiceProvider serviceProvider) : base(context, serviceProvider)
        {
            _userManager = userManager;
            _seasonBuisness = seasonBuisness;
        }

        public async Task<IViewComponentResult> InvokeAsync(int page = 0, int size = 10, GameType? type = null)
        {
            var season = _seasonBuisness.GetLastSeason();
            var currentUser = await _userManager.GetUserAsync(UserClaimsPrincipal);

            return View(new GameListComponentModel
            {
                Games = season.Games.Where(g => g.User == currentUser && (type is null || g.Type == type))
                    .OrderByDescending(g => g.DateTime)
                    .Skip(size * page).Take(size).ToList(),
                SrEvolution = ServiceProvider.GetService<SrEvolution>()
            });
        }
    }
}