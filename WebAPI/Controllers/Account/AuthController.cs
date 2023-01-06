using Domain.Models;
using Domain.Constants;
using Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebAPI.Authentication;

namespace WebAPI.Controllers.Account
{
    [Route("api/account")]
    [ApiController]
    public class AuthController: ControllerBase
    {
        private readonly IJwtTokenManager jwtTokenManager;
        private readonly ILoggerManager logger;
        public AuthController(IJwtTokenManager jwtTokenManager, ILoggerManager logger)
        {
            this.jwtTokenManager = jwtTokenManager;
            this.logger = logger;
        }

        [HttpPost("getusername")]
        public Task<string> GetUserNameByToken(Token token)
        {
            //logger.LogInfo($"IP: {ipLogin} has logged into system");
            return Task.FromResult(jwtTokenManager.GetValueClaim(token.token, USER_IDENTITY_CONST.UNIQUE_NAME));
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate(AuthenticateRequest userCredential)
        {
            IPAddress remoteIpAddress = Request.HttpContext.Connection.RemoteIpAddress;
            string ipLogin = string.Empty;
            if (remoteIpAddress != null)
            {
                // If we got an IPV6 address, then we need to ask the network for the IPV4 address 
                // This usually only happens when the browser is on the same machine as the server.
                if (remoteIpAddress.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6)
                {
                    remoteIpAddress = System.Net.Dns.GetHostEntry(remoteIpAddress).AddressList
            .First(x => x.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork);
                }
                ipLogin = remoteIpAddress.ToString();
            }

            logger.LogInfo($"IP: {ipLogin} has logged into system");
            AuthenticateResponse authenticateResponse = await jwtTokenManager.Authenticate(userCredential.UserName, userCredential.Password, ipLogin);
            return Ok(authenticateResponse);
        }
    }
    public class Token
    {
        public string token { get; set; }
    }
}
