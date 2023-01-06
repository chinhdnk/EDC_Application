using Application.Interfaces.IRepositories;
using Domain.Models;
using Domain.Constants;
using Infrastructure.Entities.AdminSystem;
using Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebAPI.Authentication;
using WebAPI.Filters;

namespace WebAPI.Controllers.Account
{
    [ApiController]
    [Route("api/[controller]")]
    [JwtAuthorize]
    public class AccountController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly AdminDBContext _dbContext;
        private readonly IAccountRepository _accountRepo;
        private readonly IJwtTokenManager _jwtTokenManager;
        public AccountController(AdminDBContext dbContext, IAccountRepository accountRepository, IJwtTokenManager jwtTokenManager, ILoggerManager logger)
        {
            _dbContext = dbContext;
            _accountRepo = accountRepository;
            _jwtTokenManager = jwtTokenManager;
            _logger = logger;
        }

        [HttpPut]
        public async Task<IActionResult> ChangePassword(ChangePasswordModel model)
        {
            _logger.LogInfo($"{model.Username} request to change password");
            //model.Username = jwtTokenManager.GetValueClaim(Request.Headers[UserIdentityConstant.TOKEN_HEADER], UserIdentityConstant.UNIQUE_NAME);
            model.Username = HttpContext.Items[USER_IDENTITY_CONST.USERNAME].ToString();
            //Changepassword
            string result = await _accountRepo.ChangePassword(model);
            if (result == "reset_pw_success")
                _logger.LogInfo($"{model.Username} change password success");
            else
                _logger.LogInfo($"{model.Username} change password error:" + result);
            return Ok(result);
        }

        [HttpPost]
        [Route("/accesslog")]
        public async Task<IActionResult> Accesslog(AccessLogModel model)
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

            TblAccessLog tblAccessLog = new TblAccessLog()
            {
                Username = HttpContext.Items[USER_IDENTITY_CONST.USERNAME].ToString(),
                Ip = ipLogin,
                Action = model.Action,
                Status = model.Status,
                LogDate = System.DateTime.Now
            };

            await _accountRepo.Accesslog(tblAccessLog);
            _logger.LogInfo($"{model.Username} log out success");
            return Ok(new ResponseValue<AccessLogModel> { IsSucess = true, ErrorMessage = "account_create_success", ObjectValue = model });
        }
    }
}
