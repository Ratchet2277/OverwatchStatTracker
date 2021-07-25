using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DAL;
using DomainModel;
using DomainModel.Types;
using Microsoft.AspNetCore.Identity;

namespace WebApplication.Buisness
{
    public class GamesBuisness : BaseBuisness
    {
        private readonly SeasonBuisness _seasonBuisness;

        public GamesBuisness(TrackerContext context, UserManager<User> userManager, ClaimsPrincipal user,
            SeasonBuisness seasonBuisness) : base(context, userManager, user)
        {
            _seasonBuisness = seasonBuisness;
        }

        public async Task<List<Game>> GetGames(int page = 0, int? size = 10, GameType? type = null)
        {
            var season = _seasonBuisness.GetLastSeason();
            var currentUser = await UserManager.GetUserAsync(User);

            var query = season.Games.Where(g => g.User == currentUser && (type is null || g.Type == type))
                .OrderByDescending(g => g.DateTime);

            return size is not null ? query.Skip(page * (int) size).Take((int) size).ToList() : query.ToList();
        }
    }
}