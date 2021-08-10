using Business.Contracts;
using DomainModel;

namespace DataModel
{
    public class GameListModel
    {
        public GameListModel(Pagination<Game> games, ISrEvolution srEvolution)
        {
            Games = games;
            SrEvolution = srEvolution;
        }

        public Pagination<Game> Games { get; private set; }

        public ISrEvolution SrEvolution { get; }
    }
}