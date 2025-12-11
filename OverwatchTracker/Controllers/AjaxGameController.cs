using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DomainModel;
using DomainModel.Types;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication.Controllers;

[Authorize]
public partial class GameController
{
    [ResponseCache(Duration = 3600)]
    [HttpGet("MapList")]
    public async Task<List<Map>> MapList()
    {
        return (await seasonBusiness.GetLastSeason()).MapPool.OrderBy(m => m.Name).ToList();
    }

    [HttpGet("RoleList")]
    [ResponseCache(Duration = 3600)]
    public Dictionary<int, string> RoleList()
    {
        return Enum.GetValues(typeof(GameType)).Cast<GameType>().ToDictionary(t => (int)t, t => t.ToString());
    }

    [HttpGet("HeroList/{roleId:int?}")]
    [ResponseCache(Duration = 3600, VaryByHeader = "roleId")]
    public async Task<List<Hero>> HeroList(int? roleId)
    {
        if (!ModelState.IsValid)
        {
            return [];
        }
        
        var season = await seasonBusiness.GetLastSeason();
        IEnumerable<Hero> query = season.HeroPool.OrderBy(h => h.Name);

        if (Enum.TryParse(roleId.ToString(), out Role role) && season.HeroPool.Any(h => h.Role == role))
            query = query.Where(h => h.Role == role);

        return query.ToList();
    }
}