using System.Collections.Generic;
using System.Linq;
using DomainModel;
using DomainModel.Types;
using WebApplication.Struct;

namespace WebApplication.Helpers
{
    public class WinRateHelper
    {
        public static Dictionary<GameType, WinRate> WrByRole(IEnumerable<Game> games)
        {
            var enumerable = games.ToList();

            return enumerable.GroupBy(g => g.Type).ToDictionary(group => group.Key, GetWinRate);
        }

        private static WinRate GetWinRate(IEnumerable<Game> games)
        {
            var enumerable = games.ToList();
            var nbWin = enumerable.Count(g => g.AllieScore > g.EnemyScore);
            var nbDraw = enumerable.Count(g => g.AllieScore == g.EnemyScore);
            var nbTotal = enumerable.Count;
            if (nbTotal == 0)
            {
                return new WinRate();
            } 
            return new WinRate
            {
                Rate = (double) nbWin / nbTotal,
                DrawRate = (double) nbDraw / nbTotal
            };
        }
    }
}