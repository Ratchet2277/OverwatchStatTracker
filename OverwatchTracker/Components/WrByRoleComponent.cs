using DAL;
using DomainModel;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Helpers;

namespace WebApplication.Components
{
    public class WrByRoleComponent : BaseComponent
    {
        public WrByRoleComponent(TrackerContext context) : base(context)
        {
        }
        
        public IViewComponentResult Invoke()
        {
            Season season = SeasonHelper.GetLastSeason(Context.Seasons);

            return View(WinRateHelper.WrByRole(season.Games));
        }
    }
}