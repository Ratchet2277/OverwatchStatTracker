using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DAL;
using DomainModel;
using DomainModel.Types;
using Microsoft.AspNetCore.Identity;

namespace WebApplication.Business
{
    public class GamesBusiness : BaseBusiness
    {
        private readonly SeasonBusiness _seasonBusiness;

        public GamesBusiness(TrackerContext context, UserManager<User> userManager, ClaimsPrincipal user,
            SeasonBusiness seasonBusiness) : base(context, userManager, user)
        {
            _seasonBusiness = seasonBusiness;
        }

        public async Task<List<Game>> GetGames(int page = 0, int? size = 10, GameType? type = null)
        {
            var season = _seasonBusiness.GetLastSeason();
            var currentUser = await UserManager.GetUserAsync(User);

            var query = season.Games.Where(g => g.User == currentUser && (type is null || g.Type == type))
                .OrderByDescending(g => g.DateTime);

            return size is not null ? query.Skip(page * (int) size).Take((int) size).ToList() : query.ToList();
        }
    }
}