using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using DAL;
using DomainModel;
using DomainModel.Types;
using Microsoft.AspNetCore.Identity;

namespace WebApplication.Buisness
{
    public class WinRateHelper : BaseBuisness
    {
        public WinRateHelper(TrackerContext context, UserManager<User> userManager, ClaimsPrincipal user) : base(
            context, userManager, user)
        {
        }

        public static Dictionary<GameType, Struct.WinRate> WrByRole(IEnumerable<Game> games)
        {
            var enumerable = games.ToList();

            return enumerable.GroupBy(g => g.Type).ToDictionary(group => group.Key, GetWinRate);
        }

        private static Struct.WinRate GetWinRate(IEnumerable<Game> games)
        {
            var enumerable = games.ToList();
            var nbWin = enumerable.Count(g => g.AllieScore > g.EnemyScore);
            var nbDraw = enumerable.Count(g => g.AllieScore == g.EnemyScore);
            var nbTotal = enumerable.Count;
            if (nbTotal == 0) return new Struct.WinRate();
            return new Struct.WinRate
            {
                Rate = (double) nbWin / nbTotal,
                DrawRate = (double) nbDraw / nbTotal
            };
        }
    }
}