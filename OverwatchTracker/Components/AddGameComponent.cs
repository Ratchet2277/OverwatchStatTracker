using System.Linq;
using DAL;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Models;

namespace WebApplication.Components
{
    public class AddGameComponent : ViewComponent
    {
        private readonly TrackerContext _context;

        public AddGameComponent(TrackerContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var model = new AddGameViewModel(_context.Seasons.OrderByDescending(s => s.Number).First());

            return View(model);
        }
    }
}