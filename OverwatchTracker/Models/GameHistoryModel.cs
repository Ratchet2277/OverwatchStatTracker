
#nullable enable
using DomainModel;
using DomainModel.Types;

namespace WebApplication.Models
{
    public class GameHistoryModel
    {
        public Pagination<Game> Games { get; }
        public GameType? Type { get; set; }
        public MapType? MapType { get; set; }
        public string[]? SquadMembers { get; set; }

        public GameHistoryModel(Pagination<Game> games)
        {
            Games = games;
        }
    }
}