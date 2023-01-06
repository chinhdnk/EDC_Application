using AutoMapper;
using Domain.Constants;
using Domain.Models;
using Infrastructure.Interfaces.IRepositories;
using Infrastructure.Repositories;
using Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WebAPI.Filters;
using WebAPI.Services;

namespace WebAPI.Controllers.Admin
{
    [ApiController]
    [Route("api/admin/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        public readonly IUserRepository _userRepo;
        private readonly IFileService _fileService;
        private readonly IConfiguration _configuration;

        public UserController(IUserRepository userRepo, ILoggerManager logger, IFileService fileService, IConfiguration configuration)
        {
            _logger = logger;
            _userRepo = userRepo;
            _fileService = fileService;
            _configuration = configuration;
        }
        [HttpGet]
        public IActionResult Get(int pageNumber, string username, string fullname, string email)
        {
            PageParameters pageParameters = new PageParameters();
            pageParameters.PageNumber = pageNumber;
            PagingResponse<UserModel> userList = _userRepo.GetUserList(pageParameters, username, fullname, email);
            return Ok(userList);
        }

        [HttpPost]
        public async Task<IActionResult> Post(UserModel user)
        {
            string username = HttpContext.User.Identity.Name;
            user.CreatedBy = username;
            user.CreatedDate = DateTime.Now;
            bool isUserExist = await _userRepo.CheckExist(user.Username);

            if (isUserExist)
            {
                _logger.LogError($"{username} create user failed because user name has existed");
                return Ok(new ResponseValue<UserModel> { IsSucess = false, ErrorMessage = "account_exist", ObjectValue = null });
            }

            isUserExist = await _userRepo.CheckExistEmail(user.Email);
            if (isUserExist)
            {
                _logger.LogError($"{username} create user failed because email has existed");
                return Ok(new ResponseValue<UserModel> { IsSucess = false, ErrorMessage = "email_exist", ObjectValue = null });
            }

            user = await _userRepo.Create(user);
            if (user == null)
            {
                _logger.LogError($"{username} create user failed");
                return Ok(new ResponseValue<UserModel> { IsSucess = false, ErrorMessage = "savechange_failed", ObjectValue = null });
            }

            _logger.LogInfo($"{username} create user {user.Username} successfully");
            return Ok(new ResponseValue<UserModel> { IsSucess = true, ErrorMessage = "account_create_success", ObjectValue = user });
        }

       

        [HttpPost("UploadImage")]
        public async Task<ActionResult<IList<UploadResult>>> UploadImage(
            [FromForm] IEnumerable<IFormFile> files)
        {
            string filePath = _configuration.GetSection(SYSTEM_PARAS.USER_PATH).Value;
            if (!Directory.Exists(filePath))
                Directory.CreateDirectory(filePath);
            var resourcePath = new Uri($"{Request.Scheme}://{Request.Host}/");
            return new CreatedResult(resourcePath, await _fileService.UploadFile(files, filePath, resourcePath.ToString()));
        }
    }
}
