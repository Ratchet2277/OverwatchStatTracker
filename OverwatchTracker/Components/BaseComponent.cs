using System;
using DAL;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication.Components
{
    public abstract class BaseComponent : ViewComponent
    {
        protected readonly TrackerContext Context;
        protected readonly IServiceProvider ServiceProvider;

        public BaseComponent(TrackerContext context, IServiceProvider serviceProvider)
        {
            Context = context;
            ServiceProvider = serviceProvider;
        }
    }
}