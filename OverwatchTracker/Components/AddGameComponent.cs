using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using DAL;
using DomainModel;
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