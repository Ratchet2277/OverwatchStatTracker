#nullable enable
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.CompilerServices;

namespace Repository;

public abstract class BaseRepository<T>
{
    protected BaseRepository(TrackerContext context)
    {
        Context = context;
    }

    protected TrackerContext Context { get; set; }

    public IQueryable<T>? Query { get; protected set; }

    public T[] ToArray()
    {
        if (Query is null) throw new IncompleteInitialization();
        return Query.ToArray();
    }

    public async Task<T[]> ToArrayAsync()
    {
        if (Query is null) throw new IncompleteInitialization();
        return await Query.ToArrayAsync();
    }

    public List<T> ToList()
    {
        if (Query is null) throw new IncompleteInitialization();
        return Query.ToList();
    }

    public async Task<List<T>> ToListAsync()
    {
        if (Query is null) throw new IncompleteInitialization();
        return await Query.ToListAsync();
    }

    public T First()
    {
        if (Query is null) throw new IncompleteInitialization();
        return Query.First();
    }

    public async Task<T> FirstAsync()
    {
        if (Query is null) throw new IncompleteInitialization();
        return await Query.FirstAsync();
    }
}