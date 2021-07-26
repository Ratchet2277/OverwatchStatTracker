using System;
using DAL;
using DomainModel;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Business;
namespace WebApplication.Components
{
    public class AddGameComponent : BaseComponent
    {
        private readonly SeasonBusiness _seasonBusiness;

        public AddGameComponent(TrackerContext context, SeasonBusiness seasonBusiness, IServiceProvider serviceProvider)
            : base(context, serviceProvider)
        {
            _seasonBusiness = seasonBusiness;
        }

        public IViewComponentResult Invoke()
        {
            return View(new Game());
        }
    }
}