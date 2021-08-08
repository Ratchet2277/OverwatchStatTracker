using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using DomainModel;
using DomainModel.Types;
using Microsoft.AspNetCore.Identity;

namespace Business
{
    public class WinRateHelper : BaseBusiness
    {
        public WinRateHelper(UserManager<User> userManager, ClaimsPrincipal user,
            IServiceProvider serviceProvider) : base(
            userManager, user, serviceProvider)
        {
        }

        public static Dictionary<GameType, DomainModel.Struct.WinRate> WrByRole(IEnumerable<Game> games)
        {
            var enumerable = games.ToList();

            return enumerable.GroupBy(g => g.Type).ToDictionary(group => group.Key, GetWinRate);
        }

        private static DomainModel.Struct.WinRate GetWinRate(IEnumerable<Game> games)
        {
            var enumerable = games.ToList();
            var nbWin = enumerable.Count(g => g.AllieScore > g.EnemyScore);
            var nbDraw = enumerable.Count(g => g.AllieScore == g.EnemyScore);
            var nbTotal = enumerable.Count;
            if (nbTotal == 0) return new DomainModel.Struct.WinRate();
            return new DomainModel.Struct.WinRate
            {
                Rate = (double)nbWin / nbTotal,
                DrawRate = (double)nbDraw / nbTotal
            };
        }
    }
}