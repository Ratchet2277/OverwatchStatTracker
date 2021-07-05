using System.Linq;
using System.Threading.Tasks;
using DAL;
using DomainModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class SquadMemberController : BaseController
    {
        public SquadMemberController(TrackerContext context, ILogger<SquadMemberController> logger, UserManager<User> userManager) : base(context, logger, userManager)
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