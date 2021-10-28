#nullable enable
using System.Threading.Tasks;
using DomainModel;

namespace Repository.Contracts
{
    public interface ISeasonRepository : IBaseRepository<Season>
    {
        public Task<Season?> LastSeason();
    }
}