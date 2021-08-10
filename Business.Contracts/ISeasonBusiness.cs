using DomainModel;

namespace Business.Contracts
{
    public interface ISeasonBusiness
    {
        public Season GetLastSeason();
    }
}