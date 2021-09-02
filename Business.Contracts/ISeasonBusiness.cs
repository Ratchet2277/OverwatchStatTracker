using System.Threading.Tasks;
using DomainModel;

namespace Business.Contracts
{
    public interface ISeasonBusiness
    {
        public Task<Season> GetLastSeason();
    }
}