using DAL;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication.Components
{
    public abstract class BaseComponent : ViewComponent
    {
        protected readonly TrackerContext Context;

        public BaseComponent(TrackerContext context)
        {
            Context = context;
        }
        
    }
}