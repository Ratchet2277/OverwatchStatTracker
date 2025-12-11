using System.Linq;
using System.Threading.Tasks;
using DAL;
using DataModel;
using DomainModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace WebApplication.Controllers;

public class SquadMemberController(
    TrackerContext context,
    ILogger<SquadMemberController> logger,
    UserManager<User> userManager)
    : BaseController(logger, userManager)
{
    public async Task<Select2Result> Search(string term)
    {
        var currentUser = await UserManager.GetUserAsync(User);
        Select2Result result = new(await context.SquadMembers
            .Where(s => s.Name.Contains(term) && s.MainUser == currentUser)
            .Select(s => s.Name)
            .ToListAsync());
        return result;
    }
}