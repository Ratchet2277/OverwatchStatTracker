#nullable enable
using System;
using Business.Contracts;
using DomainModel;
using DomainModel.Types;

namespace DataModel
{
    public class GameHistoryModel : GameListModel
    {
        public GameHistoryModel(Pagination<Game> games, ISrEvolution srEvolution) : base(games, srEvolution)
        {
        }

        public GameType? Type { get; set; }
        public MapType? MapType { get; set; }
        public string[]? SquadMembers { get; set; }
        public DateTime UpperDateTime { get; set; }
        public DateTime LowerDateTime { get; set; }
    }
}