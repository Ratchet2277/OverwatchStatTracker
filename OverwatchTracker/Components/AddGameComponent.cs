using DAL;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Helpers;
using WebApplication.Models;

namespace WebApplication.Components
{
    public class AddGameComponent : BaseComponent
    {
        public AddGameComponent(TrackerContext context) : base(context)
        {
        }

        public IViewComponentResult Invoke()
        {
            AddGameViewModel model = new(SeasonHelper.GetLastSeason(Context.Seasons));
            return View(model);
        }
    }
}