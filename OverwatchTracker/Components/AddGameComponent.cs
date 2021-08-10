using System;
using DomainModel;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication.Components
{
    public class AddGameComponent : BaseComponent
    {
        public AddGameComponent(IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
        }

        public IViewComponentResult Invoke()
        {
            return View(new Game());
        }
    }
}