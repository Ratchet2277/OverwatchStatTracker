#nullable enable
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Business.Contracts;
using DataModel;
using DomainModel;
using DomainModel.Types;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Repository.Contracts;
using ViewModel.Contract;

namespace Business
{
    public class GamesBusiness : BaseBusiness, IGameBusiness
    {
        private readonly ISeasonBusiness _seasonBusiness;
        private readonly IGameRepository _repository;

        public GamesBusiness(UserManager<User> userManager, ClaimsPrincipal user, IServiceProvider serviceProvider, 
            ISeasonBusiness seasonBusiness, IGameRepository repository) : base(userManager, user, serviceProvider)
        {
            _seasonBusiness = seasonBusiness;
            _repository = repository;
            
        }

        public async Task<IPagination<Game>> GetGames(int page = 1, int? pageSize = 10, GameType? type = null)
        {
            var season = _seasonBusiness.GetLastSeason();
            var currentUser = await UserManager.GetUserAsync(User);

            var query = season.Games.Where(g => g.User == currentUser && (type is null || g.Type == type))
                .OrderByDescending(g => g.DateTime);

            return new Pagination<Game>(query, page, pageSize);
        }

        public async Task<Game?> GetPreviousGame(Game game)
        {
            var currentUser = await UserManager.GetUserAsync(User);
            var season = _seasonBusiness.GetLastSeason();

            var previousGameQuery = _repository.Find(currentUser)
                .ByType(game.Type)
                .BySeason(season).Query?.Where(g => g.DateTime < game.DateTime && g.Type == game.Type)
                .OrderByDescending(g => g.DateTime);

            if (previousGameQuery is null)
                return null;

            if (!previousGameQuery.Any())
                return null;

            return await previousGameQuery.FirstAsync();
        }
    }
}