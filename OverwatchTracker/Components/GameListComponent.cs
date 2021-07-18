using System.Linq;
using System.Threading.Tasks;
using DAL;
using DomainModel;
using DomainModel.Types;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Buisness;
using WebApplication.Helpers;
using WebApplication.Models;

namespace WebApplication.Components
{
    public class GameListComponent : BaseComponent
    {
        private readonly UserManager<User> _userManager;

        public GameListComponent(TrackerContext context, UserManager<User> userManager) : base(context)
        {
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync(int page = 0, int size = 10, GameType? type = null)
        {
            var season = SeasonHelper.GetLastSeason(Context.Seasons);
            var currentUser = await _userManager.GetUserAsync(UserClaimsPrincipal);

            return View(new GameListComponentModel
            {
                Games = season.Games.Where(g => g.User == currentUser && (type is null || g.Type == type))
                    .Skip(size * page).Take(size).ToList(),
                SrEvolution = new SrEvolution(Context, currentUser)
            });
        }
    }
}