using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class SquadMemberController : BaseController
    {
        public SquadMemberController(TrackerContext context, ILogger<SquadMemberController> logger) : base(context,
            logger)
        {
        }

        public async Task<Select2Result> Search(string term)
        {
            Select2Result result = new(await Context.SquadMembers.Where(s => s.Name.Contains(term))
                .Select(s => s.Name)
                .ToListAsync());
            return result;
        }
    }
}