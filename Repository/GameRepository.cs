#nullable enable
using System;
using System.Linq;
using System.Threading.Tasks;
using DAL;
using DomainModel;
using DomainModel.Types;
using Microsoft.VisualBasic.CompilerServices;
using Repository.Contracts;

namespace Repository
{
    public class GameRepository : BaseRepository<Game>, IGameRepository
    {
        public GameRepository(TrackerContext context) : base(context)
        {
        }


        public async Task<Game> Get(int id)
        {
            return await Context.Games.FindAsync(id);
        }

        public async Task Add(Game game)
        {
            Context.Games.Add(game);
            await Context.SaveChangesAsync();
        }

        public async Task Update(Game game)
        {
            Context.Games.Update(game);
            await Context.SaveChangesAsync();
        }


        public IGameRepository Find(User currentUser, bool withPlacement = false)
        {
            Query = Context.Games.Where(g => g.User == currentUser);

            if (!withPlacement)
                Query = Query.Where(g => g.IsPlacement == false);

            return this;
        }

        public IGameRepository ByHero(Hero hero)
        {
            if (Query is null) throw new IncompleteInitialization();
            Query = Query.Where(g => g.Heroes.Contains(hero));
            return this;
        }

        public IGameRepository ByType(GameType gameType)
        {
            if (Query is null) throw new IncompleteInitialization();
            Query = Query.Where(g => g.Type == gameType);
            return this;
        }

        public IGameRepository BySeason(Season season)
        {
            if (Query is null) throw new IncompleteInitialization();
            Query = Query.Where(g => g.Season == season);
            return this;
        }

        public IGameRepository Win(bool invert = false)
        {
            if (Query is null) throw new IncompleteInitialization();
            Query = invert
                ? Query.Where(g => g.AllieScore ! < g.EnemyScore)
                : Query.Where(g => g.AllieScore > g.EnemyScore);
            return this;
        }

        public IGameRepository Lose(bool invert = false)
        {
            if (Query is null) throw new IncompleteInitialization();
            Query = invert
                ? Query.Where(g => g.AllieScore ! > g.EnemyScore)
                : Query.Where(g => g.AllieScore < g.EnemyScore);
            return this;
        }

        public IGameRepository Draw(bool invert = false)
        {
            if (Query is null) throw new IncompleteInitialization();
            Query = invert
                ? Query.Where(g => g.AllieScore != g.EnemyScore)
                : Query.Where(g => g.AllieScore == g.EnemyScore);
            return this;
        }

        public IGameRepository ByDayOfWeek(DayOfWeek dayOfWeek)
        {
            if (Query is null) throw new IncompleteInitialization();
            Query = Query.Where(g => g.DateTime.DayOfWeek == dayOfWeek);
            return this;
        }

        public IGameRepository OrderByDate(bool isDescending = true)
        {
            if (Query is null) throw new IncompleteInitialization();
            Query = isDescending
                ? Query.OrderBy(g => g.DateTime)
                : Query.OrderByDescending(g => g.DateTime);

            return this;
        }

        public bool Any()
        {
            return Query is not null && Query.Any();
        }
    }
}