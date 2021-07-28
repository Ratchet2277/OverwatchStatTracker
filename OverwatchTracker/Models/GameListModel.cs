using DomainModel;
using WebApplication.Business;

namespace WebApplication.Models
{
    public class GameListModel
    {
        public GameListModel(Pagination<Game> games, SrEvolution srEvolution)
        {
            Games = games;
            SrEvolution = srEvolution;
        }
        public Pagination<Game> Games { get; private set; }

        public SrEvolution SrEvolution { get; private set; }
    }
}