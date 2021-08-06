using System;
using DAL;

namespace Repository
{
    public abstract class BaseRepository
    {
        protected TrackerContext Context { get; set; }

        public BaseRepository(TrackerContext context)
        {
            Context = context;
        }
    }
}