using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.Contracts
{
    public interface IBaseRepository<T>
    {
        public T[] ToArray();
        public Task<T[]> ToArrayAsync();
        public List<T> ToList();
        public Task<List<T>> ToListAsync();
        public Task<T> Get(int id);
        public Task Add(T entity);

        public Task Update(T entity);
    }
}