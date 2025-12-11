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

namespace Business;

public class GamesBusiness(
    UserManager<User> userManager,
    ClaimsPrincipal user,
    ISeasonBusiness seasonBusiness,
    IGameRepository repository,
    TrackerContext context,
    ISquadMemberBusiness squadMemberBusiness)
    : BaseBusiness(userManager, user), IGameBusiness
{
    public async Task<IPagination<Game>> GetGames(int page = 1, int? pageSize = 10, GameType? type = null)
    {
        var season = await seasonBusiness.GetLastSeason();
        var currentUser = await UserManager.GetUserAsync(UClaimsPrincipal);

        var query = season.Games.Where(g => g.User == currentUser && (type is null || g.Type == type))
            .OrderByDescending(g => g.DateTime);

        return new Pagination<Game>(query, page, pageSize);
    }

    public async Task<Game?> GetPreviousGame(Game game, bool allowPlacement = false)
    {
        var currentUser = await UserManager.GetUserAsync(UClaimsPrincipal);
        var season = await seasonBusiness.GetLastSeason();

        var previousGameQuery = repository.Find(currentUser, allowPlacement)
            .ByType(game.Type)
            .BySeason(season).Query?.Where(g => g.DateTime < game.DateTime && g.Type == game.Type)
            .OrderByDescending(g => g.DateTime);

        if (previousGameQuery is null)
            return null;

        if (!await previousGameQuery.AnyAsync())
            return null;

        var previousGame = await previousGameQuery.FirstAsync();

        return previousGame;
    }

    public async Task<Game?> Get(int id)
    {
        return await repository.Get(id);
    }

    public async Task Add(Game entity)
    {
        if (entity.User is null)
            throw new ArgumentException("Game.User must be set before being added to the repository");

        entity.DateTime = DateTime.Now;
        entity.Map = await context.Maps.FindAsync(entity.NewMap);
        var newHeroes = entity.NewHeroes;
        entity.Heroes =
            new Collection<Hero>(await context.Heroes.Where(h => newHeroes.Contains(h.Id)).ToListAsync());

        if (entity.NewSquadMembers.Length > 0)
            squadMemberBusiness.EditSquadMemberList(ref entity, entity.NewSquadMembers);

        await repository.Add(entity);
    }

    public async Task Update(Game entity)
    {
        var game = await repository.Get(entity.Id);

        if (game is null)
            throw new ArgumentException("Game not found in database");

        game.AllieScore = entity.AllieScore;
        game.EnemyScore = entity.EnemyScore;
        game.Type = entity.Type;
        game.Sr = entity.Sr;
        game.IsPlacement = entity.IsPlacement;

        if (game.Map.Id != entity.NewMap) game.Map = await context.Maps.FindAsync(entity.NewMap);

        foreach (var heroToDel in game.Heroes.Where(h => !entity.NewHeroes.Contains(h.Id)).ToList())
            game.Heroes.Remove(heroToDel);

        foreach (var heroToAdd in context.Heroes.Where(h =>
                     entity.NewHeroes.Contains(h.Id) && !game.Heroes.Contains(h))) game.Heroes.Add(heroToAdd);

        squadMemberBusiness.EditSquadMemberList(ref game, entity.NewSquadMembers);

        await repository.Update(game);
    }
}