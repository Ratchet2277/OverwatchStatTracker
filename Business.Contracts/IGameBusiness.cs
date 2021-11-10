#nullable enable
using System.Threading.Tasks;
using DomainModel;
using DomainModel.Types;
using ViewModel.Contract;

namespace Business.Contracts;

public interface IGameBusiness
{
    public Task<IPagination<Game>> GetGames(int page = 1, int? pageSize = 10, GameType? type = null);
    public Task<Game?> GetPreviousGame(Game game, bool allowPlacement = false);

    public Task<Game?> Get(int id);

    public Task Add(Game entity);

    public Task Update(Game entity);
}