#nullable enable
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Business.Contracts;
using DAL;
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
        private readonly TrackerContext _context;
        private readonly IGameRepository _repository;
        private readonly ISquadMemberBusiness _squadMemberBusiness;
        private readonly Task<Season> _currentSeason;
        public GamesBusiness(UserManager<User> userManager, ClaimsPrincipal user,
            ISeasonBusiness seasonBusiness, IGameRepository repository, TrackerContext context,
            ISquadMemberBusiness squadMemberBusiness) : base(userManager, user)
        {
            _currentSeason = seasonBusiness.GetLastSeason();
            _repository = repository;
            _context = context;
            _squadMemberBusiness = squadMemberBusiness;
        }

        public async Task<IPagination<Game>> GetGames(int page = 1, int? pageSize = 10, GameType? type = null)
        {
            var season = await _currentSeason;
            var currentUser = await CurrentUser;

            var query = season.Games.Where(g => g.User == currentUser && (type is null || g.Type == type))
                .OrderByDescending(g => g.DateTime);

            return new Pagination<Game>(query, page, pageSize);
        }

        public async Task<Game?> GetPreviousGame(Game game, bool allowPlacement = false)
        {
            var currentUser = await CurrentUser;
            var season = await _currentSeason;

            var previousGameQuery = _repository.Find(currentUser, allowPlacement)
                .ByType(game.Type)
                .BySeason(season).Query?.Where(g => g.DateTime < game.DateTime && g.Type == game.Type)
                .OrderByDescending(g => g.DateTime);

            if (previousGameQuery is null)
                return null;

            if (!previousGameQuery.Any())
                return null;

            var previousGame = await previousGameQuery.FirstAsync();

            return previousGame;
        }

        public async Task<Game> Get(int id)
        {
            return await _repository.Get(id);
        }

        public async Task Add(Game game)
        {
            if (game.User is null)
                throw new ArgumentException("Game.User must be set before being added to the repository");

            game.DateTime = DateTime.Now;
            game.Map = await _context.Maps.FindAsync(game.NewMap);
            var newHeroes = game.NewHeroes;
            game.Heroes =
                new Collection<Hero>(await _context.Heroes.Where(h => newHeroes.Contains(h.Id)).ToListAsync());

            if (game.NewSquadMembers.Length > 0)
                _squadMemberBusiness.EditSquadMemberList(ref game, game.NewSquadMembers);

            await _repository.Add(game);
        }

        public async Task Update(Game newGame)
        {
            var game = await _repository.Get(newGame.Id);

            if (game is null)
                throw new ArgumentException("Game not found in database");

            game.AllieScore = newGame.AllieScore;
            game.EnemyScore = newGame.EnemyScore;
            game.Type = newGame.Type;
            game.Sr = newGame.Sr;
            game.IsPlacement = newGame.IsPlacement;

            if (game.Map.Id != newGame.NewMap) game.Map = await _context.Maps.FindAsync(newGame.NewMap);

            foreach (var heroToDel in game.Heroes.Where(h => !newGame.NewHeroes.Contains(h.Id)).ToList())
                game.Heroes.Remove(heroToDel);

            foreach (var heroToAdd in _context.Heroes.Where(h =>
                newGame.NewHeroes.Contains(h.Id) && !game.Heroes.Contains(h))) game.Heroes.Add(heroToAdd);

            _squadMemberBusiness.EditSquadMemberList(ref game, newGame.NewSquadMembers);

            await _repository.Update(game);
        }
    }
}