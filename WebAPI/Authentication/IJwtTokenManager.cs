using Domain.Models;
using Infrastructure.Entities.AdminSystem;
using System.Security.Claims;

namespace WebAPI.Authentication
{
    public interface IJwtTokenManager
    {
        Task<AuthenticateResponse> Authenticate(string userName, string password, string ip);
        string GenerateRefreshToken();
        string GetValueClaim(string token, string claimType);
        Task<AuthenticateResponse> RefreshToken(TblAccount userName);
        ClaimsPrincipal VerifyToken(string token);
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
