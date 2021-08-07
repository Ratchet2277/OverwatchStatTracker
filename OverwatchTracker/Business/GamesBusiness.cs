﻿using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DomainModel;
using DomainModel.Types;
using Microsoft.AspNetCore.Identity;
using WebApplication.Models;

namespace WebApplication.Business
{
    public class GamesBusiness : BaseBusiness
    {
        private readonly SeasonBusiness _seasonBusiness;

        public GamesBusiness(UserManager<User> userManager, ClaimsPrincipal user,
            SeasonBusiness seasonBusiness) : base(userManager, user)
        {
            _seasonBusiness = seasonBusiness;
        }

        public async Task<Pagination<Game>> GetGames(int page = 1, int? pageSize = 10, GameType? type = null)
        {
            var season = _seasonBusiness.GetLastSeason();
            var currentUser = await UserManager.GetUserAsync(User);

            var query = season.Games.Where(g => g.User == currentUser && (type is null || g.Type == type))
                .OrderByDescending(g => g.DateTime);

            return new Pagination<Game>(query, page, pageSize);
        }
    }
}