using System;
using DAL;
using DomainModel;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Buisness;
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
            return View(new Game());
        }
    }
}