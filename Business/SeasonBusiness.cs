using System.Data;
using System.Security.Claims;
using System.Threading.Tasks;
using Business.Contracts;
using DomainModel;
using Microsoft.AspNetCore.Identity;
using Repository.Contracts;

namespace Business;

public class SeasonBusiness(ISeasonRepository repository, UserManager<User> userManager, ClaimsPrincipal user)
    : BaseBusiness(userManager, user), ISeasonBusiness
{
    public async Task<Season> GetLastSeason()
    {
        var season = await repository.LastSeason();
        return season ?? throw new DataException("No ranked season not found");
    }
}