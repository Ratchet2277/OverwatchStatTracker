using System;
using DAL;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Buisness;
using WebApplication.Models;

namespace WebApplication.Components
{
    public class AddGameComponent : BaseComponent
    {
        private readonly SeasonBuisness _seasonBuisness;

        public AddGameComponent(TrackerContext context, SeasonBuisness seasonBuisness, IServiceProvider serviceProvider)
            : base(context, serviceProvider)
        {
            _seasonBuisness = seasonBuisness;
        }

        public IViewComponentResult Invoke()
        {
            AddGameViewModel model = new(_seasonBuisness.GetLastSeason());
            return View(model);
        }
    }
}