using System.Collections.Generic;
using DomainModel;
using DomainModel.Types;

namespace Repository.Contracts
{
    public interface IGameRepository : IBaseRepository
    {
        public IEnumerable<Game> GetAllGames(User user, GameType? type = null);
        public IEnumerable<Game> GetAllWin(User user, GameType? type = null);
        public IEnumerable<Game> GetAllLose(User user, GameType? type = null);
        public IEnumerable<Game> GetAllDraw(User user, GameType? type = null);

        public void Find(User user);
        public void ByHero(Hero hero);
        public void ByType(GameType hero);
        public void BySeason(Season season);
        public void Win(bool invert = false);
        public void Lose(bool invert = false);
        public void Draw(bool invert = false);
    }
}