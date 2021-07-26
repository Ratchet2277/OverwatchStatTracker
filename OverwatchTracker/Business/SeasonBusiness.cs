using System.Linq;
using System.Security.Claims;
using DAL;
using DomainModel;
using Microsoft.AspNetCore.Identity;

namespace WebApplication.Business
{
    public class SeasonBusiness : BaseBusiness
    {
        public SeasonBusiness(TrackerContext context, UserManager<User> userManager, ClaimsPrincipal user) : base(
            context, userManager, user)
        {
        }

        public Season GetLastSeason()
        {
            return Context.Seasons.OrderByDescending(s => s.Number).First();
        }
    }
}