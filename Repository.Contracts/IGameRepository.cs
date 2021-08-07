using DomainModel;
using DomainModel.Types;

namespace Repository.Contracts
{
    public interface IGameRepository : IBaseRepository<Game>
    {
        public void Find(User user);
        public void ByHero(Hero hero);
        public void ByType(GameType hero);
        public void BySeason(Season season);
        public void Win(bool invert = false);
        public void Lose(bool invert = false);
        public void Draw(bool invert = false);
    }
}