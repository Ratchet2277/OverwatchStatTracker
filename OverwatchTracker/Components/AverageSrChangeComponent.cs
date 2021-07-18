using System.Threading.Tasks;
using DAL;
using DomainModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Buisness;

namespace WebApplication.Components
{
    public class AverageSrChangeComponent : BaseComponent
    {
        private readonly UserManager<User> _userManager;
        
        public AverageSrChangeComponent(TrackerContext context, UserManager<User> userManager) : base(context)
        {
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var currentUser = await _userManager.GetUserAsync(UserClaimsPrincipal);
            var srEvolution = new SrEvolution(Context, currentUser);
            return View(srEvolution.GetAverageEvolution());
        }

    }
}