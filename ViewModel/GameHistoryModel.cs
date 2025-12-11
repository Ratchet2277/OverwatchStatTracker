#nullable enable
using System;
using Business.Contracts;
using DomainModel;
using DomainModel.Types;

namespace DataModel;

public class GameHistoryModel(Pagination<Game> games, ISrEvolutionBusiness srEvolutionBusiness)
    : GameListModel(games,
        srEvolutionBusiness)
{
    public GameType? Type { get; set; }
    public MapType? MapType { get; set; }
    public string[]? SquadMembers { get; set; }
    public DateTime UpperDateTime { get; set; }
    public DateTime LowerDateTime { get; set; }
}