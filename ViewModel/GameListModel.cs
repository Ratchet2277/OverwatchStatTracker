using Business.Contracts;
using DomainModel;

namespace DataModel;

public class GameListModel(Pagination<Game> games, ISrEvolutionBusiness srEvolutionBusiness)
{
    public Pagination<Game> Games { get; } = games;

    public ISrEvolutionBusiness SrEvolutionBusiness { get; } = srEvolutionBusiness;
}