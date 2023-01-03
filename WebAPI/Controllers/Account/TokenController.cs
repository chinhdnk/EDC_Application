using Domain.Models;
using Infrastructure.Entities.AdminSystem;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Authentication;

namespace WebAPI.Controllers.Account
{
    [Route("api/token")]
    [ApiController]
    public class TokenController: ControllerBase
    {
        private readonly IJwtTokenManager _jwtTokenManager;
        private readonly AdminDBContext _dbContext;
        public TokenController(IJwtTokenManager jwtTokenManager, AdminDBContext dbContext)
        {
            _jwtTokenManager = jwtTokenManager;
            _dbContext = dbContext;
        }

        [HttpPost("refreshtoken")]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokenModel tokenDto)
        {
            if (tokenDto is null)
            {
                return BadRequest(new AuthenticateResponse { IsAuthSuccessful = false, ErrorMessage = "Invalid client request" });
            }
            var principal = _jwtTokenManager.GetPrincipalFromExpiredToken(tokenDto.Token);
            var username = principal.Identity.Name;
            var account = await _dbContext.TblAccounts.FindAsync(username);
            if (account == null || account.RefreshToken != tokenDto.RefreshToken || account.RefreshTokenExpiryTime <= DateTime.Now)
                return Forbid();

            AuthenticateResponse resultRefreshToken = await _jwtTokenManager.RefreshToken(account);
            return Ok(resultRefreshToken);
        }
    }
}
