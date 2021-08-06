namespace Repository.Contracts
{
    public interface IBaseRepository
    {
        public void ToArray();
        public void ToArrayAsync();
        public void ToList();
        public void ToListAsync();
    }
}