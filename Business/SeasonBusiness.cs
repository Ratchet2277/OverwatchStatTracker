using System;
using System.Linq;
using System.Security.Claims;
using Business.Contracts;
using DAL;
using DomainModel;
using Microsoft.AspNetCore.Identity;

namespace Business
{
    public class SeasonBusiness : BaseBusiness, ISeasonBusiness
    {
        private readonly TrackerContext _context;

        public SeasonBusiness(TrackerContext context, UserManager<User> userManager, ClaimsPrincipal user,
            IServiceProvider serviceProvider) : base(
            userManager, user, serviceProvider)
        {
            _context = context;
        }

        public Season GetLastSeason()
        {
            return _context.Seasons.OrderByDescending(s => s.Number).First();
        }
    }
}