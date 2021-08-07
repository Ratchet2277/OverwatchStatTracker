using System.Linq;
using System.Security.Claims;
using Business;
using DAL;
using DomainModel;
using Microsoft.AspNetCore.Identity;

namespace WebApplication.Business
{
    public class SeasonBusiness : BaseBusiness
    {
        private readonly TrackerContext _context;

        public SeasonBusiness(TrackerContext context, UserManager<User> userManager, ClaimsPrincipal user) : base(
            userManager, user)
        {
            _context = context;
        }

        public Season GetLastSeason()
        {
            return _context.Seasons.OrderByDescending(s => s.Number).First();
        }
    }
}