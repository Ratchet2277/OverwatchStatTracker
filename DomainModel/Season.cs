using System.Collections.ObjectModel;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace DomainModel
{
    public class Season
    {
        
        private readonly ILazyLoader _lazyLoader;

        public Season()
        {
            
        }
        public Season(ILazyLoader lazyLoader)
        {
            _lazyLoader = lazyLoader;
        }
        
        public int Id { get; set; }
        public int Number { get; set; }

        private Collection<Hero> _heroPool;

        public Collection<Hero> HeroPool
        {
            get => _lazyLoader.Load(this, ref _heroPool); 
            set => _heroPool = value;
        }

        private Collection<Map> _mapPool;

        public virtual Collection<Map> MapPool
        {
            get => _lazyLoader.Load(this, ref _mapPool); 
            set => _mapPool = value;
        }
    }
}