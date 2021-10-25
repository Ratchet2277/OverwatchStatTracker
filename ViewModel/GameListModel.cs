using Business.Contracts;
using DomainModel;

namespace DataModel;

public class GameListModel
{
    public GameListModel(Pagination<Game> games, ISrEvolutionBusiness srEvolutionBusiness)
    {
        Games = games;
        SrEvolutionBusiness = srEvolutionBusiness;
    }

    public Pagination<Game> Games { get; }

    public ISrEvolutionBusiness SrEvolutionBusiness { get; }
}