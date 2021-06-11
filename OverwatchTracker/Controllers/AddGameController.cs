using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL;
using DomainModel;
using DomainModel.Types;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace WebApplication.Controllers
{
    public class AddGameController : BaseController
    {
        public AddGameController(TrackerContext context, ILogger<AddGameController> logger) : base(context, logger)
        {
        }

        [ResponseCache(Duration = 3600)]
        [HttpGet]
        public async Task<List<Map>> MapList()
        {
            return (await Context.Seasons.OrderByDescending(s => s.Number).FirstAsync()).MapPool
                .Select(m => new Map {Name = m.Name, Id = m.Id, Type = m.Type, ImageUrl = m.ImageUrl}).ToList();
        }

        [HttpGet]
        [ResponseCache(Duration = 3600)]
        public Dictionary<int, string> RoleList()
        {
            return Enum.GetValues(typeof(GameType)).Cast<GameType>().ToDictionary(t => (int) t, t => t.ToString());
        }

        [HttpGet("AddGame/HeroList/{roleId:int?}")]
        [ResponseCache(Duration = 3600, VaryByHeader = "roleId")]
        public async Task<List<Hero>> HeroList(int? roleId)
        {
            Season season = await Context.Seasons.OrderByDescending(s => s.Number).FirstAsync();
            Role role;
            IEnumerable<Hero> query = season.HeroPool.OrderBy(h => h.Name);

            if (Enum.TryParse(roleId.ToString(), out role) && season.HeroPool.Any(h => h.Role == role))
            {
                query = query.Where(h => h.Role == role);
            }

            return query.Select(m => new Hero {Name = m.Name, Id = m.Id, Role = m.Role, ImageUrl = m.ImageUrl}).ToList();
        }
    }
}