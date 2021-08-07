using System;
using System.Collections.Generic;
using System.Linq;
using DomainModel;
using DomainModel.Types;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication.Controllers
{
    [Authorize]
    public partial class GameController
    {
        [ResponseCache(Duration = 3600)]
        [HttpGet("MapList")]
        public List<Map> MapList()
        {
            return _seasonBusiness.GetLastSeason().MapPool.OrderBy(m => m.Name).ToList();
        }

        [HttpGet("RoleList")]
        [ResponseCache(Duration = 3600)]
        public Dictionary<int, string> RoleList()
        {
            return Enum.GetValues(typeof(GameType)).Cast<GameType>().ToDictionary(t => (int)t, t => t.ToString());
        }

        [HttpGet("HeroList/{roleId:int?}")]
        [ResponseCache(Duration = 3600, VaryByHeader = "roleId")]
        public List<Hero> HeroList(int? roleId)
        {
            var season = _seasonBusiness.GetLastSeason();
            IEnumerable<Hero> query = season.HeroPool.OrderBy(h => h.Name);

            if (Enum.TryParse(roleId.ToString(), out Role role) && season.HeroPool.Any(h => h.Role == role))
                query = query.Where(h => h.Role == role);

            return query.ToList();
        }
    }
}