#nullable enable
using System;
using DomainModel;
using DomainModel.Types;
using WebApplication.Business;

namespace WebApplication.Models
{
    public class GameHistoryModel : GameListModel
    {
        public GameType? Type { get; set; }
        public MapType? MapType { get; set; }
        public string[]? SquadMembers { get; set; }
        public DateTime UpperDateTime { get; set; }
        public DateTime LowerDateTime { get; set; }

        public GameHistoryModel(Pagination<Game> games, SrEvolution srEvolution) : base(games, srEvolution)
        {
        }
    }
}