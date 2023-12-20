using Microsoft.AspNetCore.Identity;

namespace KfcApi.Repositories
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser user, List<string> roles);
    }
}
