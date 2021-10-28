using System;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication.Components
{
    public abstract class BaseComponent : ViewComponent
    {
        protected readonly IServiceProvider ServiceProvider;

        public BaseComponent(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }
    }
}