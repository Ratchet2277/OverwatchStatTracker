using System.Data;
using System.Security.Claims;
using System.Threading.Tasks;
using Business.Contracts;
using DomainModel;
using Microsoft.AspNetCore.Identity;
using Repository.Contracts;

namespace Business;

public class SeasonBusiness : BaseBusiness, ISeasonBusiness
{
    private readonly ISeasonRepository _repository;

    public SeasonBusiness(ISeasonRepository repository, UserManager<User> userManager, ClaimsPrincipal user) : base(
        userManager, user)
    {
        _repository = repository;
    }

    public async Task<Season> GetLastSeason()
    {
        var season = await _repository.LastSeason();
        if (season is null)
            throw new DataException("No ranked season not found");
        return season;
    }
}