using System.Collections.Generic;
using System.Linq;
using DomainModel;
using DomainModel.Types;

namespace Business.WinRate
{
    public partial class WinRateBusiness
    {
        private static Dictionary<GameType, DomainModel.Struct.WinRate> WrByRole(IEnumerable<Game> games)
        {
            return games.GroupBy(g => g.Type)
                .ToDictionary(group => group.Key, GetWinRate);
        }

        private static DomainModel.Struct.WinRate GetWinRate(IEnumerable<Game> games)
        {
            // var enumerable = games.ToList();
            var nbWin = games.Count(g => g.AllieScore > g.EnemyScore);
            var nbDraw = games.Count(g => g.AllieScore == g.EnemyScore);
            var nbTotal = games.Count();
            if (nbTotal == 0) return new DomainModel.Struct.WinRate();
            return new DomainModel.Struct.WinRate
            {
                Rate = (double)nbWin / nbTotal,
                DrawRate = (double)nbDraw / nbTotal
            };
        }
    }
}