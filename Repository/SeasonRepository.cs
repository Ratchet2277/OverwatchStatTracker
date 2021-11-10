#nullable enable
using System.Linq;
using System.Threading.Tasks;
using DAL;
using DomainModel;
using Microsoft.EntityFrameworkCore;
using Repository.Contracts;

namespace Repository;

public class SeasonRepository : BaseRepository<Season>, ISeasonRepository
{
    public SeasonRepository(TrackerContext context) : base(context)
    {
    }

    public async Task<Season?> Get(int id)
    {
        return await Context.Seasons.FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task Add(Season season)
    {
        Context.Seasons.Add(season);
        await Context.SaveChangesAsync();
    }

    public async Task Update(Season season)
    {
        Context.Seasons.Update(season);
        await Context.SaveChangesAsync();
    }

    public async Task<Season?> LastSeason()
    {
        return await Context.Seasons.OrderByDescending(s => s.Number).FirstAsync();
    }
}