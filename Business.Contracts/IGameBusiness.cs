#nullable enable
using System.Threading.Tasks;
using DomainModel;
using DomainModel.Types;
using ViewModel.Contract;

namespace Business.Contracts
{
    public interface IGameBusiness
    {
        public Task<IPagination<Game>> GetGames(int page = 1, int? pageSize = 10, GameType? type = null);
        public Task<Game?> GetPreviousGame(Game game);

    }
}