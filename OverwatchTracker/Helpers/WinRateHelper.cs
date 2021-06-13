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
            return new Dictionary<GameType, WinRate>
            {
                {GameType.Damage, GetWinRate(enumerable.Where(g => g.Type == GameType.Damage))},
                {GameType.Support, GetWinRate(enumerable.Where(g => g.Type == GameType.Support))},
                {GameType.Tank, GetWinRate(enumerable.Where(g => g.Type == GameType.Tank))},
                {GameType.OpenQueue, GetWinRate(enumerable.Where(g => g.Type == GameType.OpenQueue))}
            };
        }

        public static WinRate GetWinRate(IEnumerable<Game> games)
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