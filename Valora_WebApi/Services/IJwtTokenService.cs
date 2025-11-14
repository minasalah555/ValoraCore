using Valora.Models;

namespace Valora.Services
{
    public interface IJwtTokenService
    {
        Task<string> GenerateTokenAsync(ApplicationUser user);
        DateTime GetTokenExpiration();
    }
}
