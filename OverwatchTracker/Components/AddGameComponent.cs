using System.Linq;
using DAL;
using Microsoft.AspNetCore.Mvc;
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
            AddGameViewModel model = new(Context.Seasons.OrderByDescending(s => s.Number).First());
            return View(model);
        }
    }
}