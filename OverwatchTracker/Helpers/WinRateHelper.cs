using System;
using System.Collections.Generic;
using System.Linq;
using DomainModel;
using DomainModel.Types;
using WebApplication.Models.WinRate;
using WebApplication.Struct;

namespace WebApplication.Helpers
{
    public class WinRateHelper
    {
        public static WrByRole WrByRole(IEnumerable<Game> games)
        {
            var enumerable = games.ToList();
            return new WrByRole()
            {
                Damage = GetWinRate(enumerable.Where(g => g.Type == GameType.Damage)),
                Support = GetWinRate(enumerable.Where(g => g.Type == GameType.Support)),
                Tank = GetWinRate(enumerable.Where(g => g.Type == GameType.Tank)),
                OpenQueue = GetWinRate(enumerable.Where(g => g.Type == GameType.OpenQueue))
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