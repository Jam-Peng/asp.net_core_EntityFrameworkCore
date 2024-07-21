using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace createWebApi_DominModels.Repositories
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser user, List<string> roles);
    }
}
