using DAL;

namespace Repository
{
    public abstract class BaseRepository
    {
        public BaseRepository(TrackerContext context)
        {
            Context = context;
        }

        protected TrackerContext Context { get; set; }
    }
}