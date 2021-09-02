#nullable enable
using System.Linq;
using DomainModel;
using DomainModel.Types;

namespace Repository.Contracts
{
    public interface IGameRepository : IBaseRepository<Game>
    {
        public IGameRepository Find(User user, bool withPlacement = false);
        public IGameRepository ByHero(Hero hero);
        public IGameRepository ByType(GameType hero);
        public IGameRepository BySeason(Season season);
        public IGameRepository Win(bool invert = false);
        public IGameRepository Lose(bool invert = false);
        public IGameRepository Draw(bool invert = false);

        public IQueryable<Game>? Query { get; }

        public IGameRepository OrderByDate(bool isDescending = true);

        public bool Any();
    }
}