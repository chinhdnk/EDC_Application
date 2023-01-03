using Application.Interfaces.IRepositories;
using Domain.Models;
using Infrastructure.Constants;
using Infrastructure.Entities.AdminSystem;
using Infrastructure.Interfaces.IRepositories;
using BC = BCrypt.Net.BCrypt;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace WebAPI.Authentication
{
    public class JwtTokenManager : IJwtTokenManager
    {
        private JwtSecurityTokenHandler _tokenHandler;
        private readonly IConfiguration _configuration;
        private readonly AdminDBContext _dbContext;
        private readonly IPermissionRepository _permissionRepo;
        private readonly IAccountRepository _accountRepo;
        private byte[] _secrectKey;
        public JwtTokenManager(IConfiguration configuration, AdminDBContext dbContext, IPermissionRepository permissionRepository, IAccountRepository accountRepository)
        {

            _configuration = configuration;
            _dbContext = dbContext;
            _permissionRepo = permissionRepository;
            _accountRepo = accountRepository;
            _tokenHandler = new JwtSecurityTokenHandler();
            _secrectKey = Encoding.ASCII.GetBytes(configuration["Jwt:Key"]);
        }

        public async Task<AuthenticateResponse> Authenticate(string userName, string password, string ip)
        {
            AuthenticateResponse authenticateResponse = new AuthenticateResponse();
            try
            {

                TblAccount account = await _dbContext.TblAccounts.FindAsync(userName);
                TblAccessLog accesslog = new TblAccessLog();

                if (account == null ||(!BC.Verify(account.Salt + password, account.Password)))
                {
                    authenticateResponse.IsAuthSuccessful = false;
                    authenticateResponse.ErrorMessage = "username_pw_incorrect";
                    authenticateResponse.Token = string.Empty;

                    accesslog.Status = 0;
                }
                else
                {
                    var signingCredentials = GetSigningCredentials();
                    var claims = SetClaims(account);
                    var tokenOptions = GenerateTokenOptions(signingCredentials, claims);
                    var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

                    account.RefreshToken = GenerateRefreshToken();
                    account.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);
                    account.WrongTime = 0;
                    await _accountRepo.UpdateAccount(account);

                    authenticateResponse.IsAuthSuccessful = true;
                    authenticateResponse.Token = token;
                    authenticateResponse.ErrorMessage = "login_success";
                    authenticateResponse.RefreshToken = account.RefreshToken;

                    accesslog.Status = 1;
                }

                accesslog.Action = 1;
                accesslog.Ip = ip;
                accesslog.LogDate = DateTime.Now;
                accesslog.Username = userName;
                
                _dbContext.Add(accesslog);
                _dbContext.SaveChanges();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                authenticateResponse.IsAuthSuccessful = false;
                authenticateResponse.ErrorMessage = "login_fail";
            }
            return authenticateResponse;
        }

        private SigningCredentials GetSigningCredentials()
        {
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
            var secret = new SymmetricSecurityKey(key);

            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }
        private List<Claim> SetClaims(TblAccount account)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, account.Username),
                new Claim(USER_IDENTITY_CONST.FULL_NAME, account.FullName),
                new Claim(USER_IDENTITY_CONST.EMAIL, account.Email),
                new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString())
            };

            IQueryable<string> listPerm = _permissionRepo.GetPermOfUser(account.Username);
            foreach (var role in listPerm)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            return claims;
        }
        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(_secrectKey),
                ValidateLifetime = false,
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidAudience = _configuration["Jwt:Audience"],
                ValidIssuer = _configuration["Jwt:Issuer"],
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);

            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token");
            }

            return principal;
        }

        public string GetValueClaim(string token, string claimType)
        {
            if (string.IsNullOrWhiteSpace(token)) return null;
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.ReadToken(token.Replace("\"", string.Empty)) as JwtSecurityToken;

            var stringClaimValue = securityToken.Claims.First(claim => claim.Type == claimType).Value;
            return stringClaimValue;
        }

        public async Task<AuthenticateResponse> RefreshToken(TblAccount account)
        {
            AuthenticateResponse authenticateResponse = new AuthenticateResponse();
            try
            {
                var signingCredentials = GetSigningCredentials();
                var claims = SetClaims(account);
                var tokenOptions = GenerateTokenOptions(signingCredentials, claims);
                var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

                account.RefreshToken = GenerateRefreshToken();
                account.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);
                await _accountRepo.UpdateAccount(account);

                authenticateResponse.IsAuthSuccessful = true;
                authenticateResponse.Token = token;
                authenticateResponse.RefreshToken = account.RefreshToken;
            }
            catch (Exception)
            {
                authenticateResponse.IsAuthSuccessful = false;
                authenticateResponse.ErrorMessage = "Invalid client request";
            }
            return authenticateResponse;
        }

        public ClaimsPrincipal VerifyToken(string token)
        {
            if (string.IsNullOrWhiteSpace(token)) return null;

            SecurityToken securityToken;

            try
            {
                var principal = _tokenHandler.ValidateToken(
                token.Replace("\"", string.Empty),
                new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(_secrectKey),
                    ValidateLifetime = true,
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ClockSkew = TimeSpan.Zero
                },
                out securityToken);
                var jwtToken = (JwtSecurityToken)securityToken;
                if (jwtToken == null || !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                StringComparison.InvariantCultureIgnoreCase))
                {
                    throw new SecurityTokenException("Invalid token");
                }

                return principal;
            }
            catch (SecurityTokenException)
            {
                throw new SecurityTokenException("Invalid token");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var tokenOptions = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(Int32.Parse(_configuration["Jwt:ExpireMinutes"])),
                signingCredentials: signingCredentials);
            return tokenOptions;
        }
    }
}
