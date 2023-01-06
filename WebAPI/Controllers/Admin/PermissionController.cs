using Domain.Models;
using Infrastructure.Entities.AdminSystem;
using Infrastructure.Interfaces.IRepositories;
using Infrastructure.Repositories;
using Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security;

namespace WebAPI.Controllers.Admin
{
    [ApiController]
    [Route("api/admin/[controller]")]
    public class PermissionController : ControllerBase
    {
        private readonly IPermissionRepository _permRepo;
        private readonly ILoggerManager _logger;
        private string userName = string.Empty;
        public PermissionController(IPermissionRepository permRepo, ILoggerManager logger)
        {
            _permRepo = permRepo;
            _logger = logger;
            
        }

        [HttpGet]
        public async Task<IActionResult> Get(int status, int pageNumber, string searchKey )
        {
            PageParameters pageParameters = new PageParameters();
            pageParameters.PageNumber = pageNumber;
            userName = HttpContext.User.Identity.Name;
            PagingResponse<PermissionModel> permLists = await _permRepo.GetPermissionList(pageParameters, searchKey, status == 1);
            _logger.LogInfo($"{userName} get the permission list");
            return Ok(permLists);
        }

        [HttpPut]
        public async Task<IActionResult> Put(PermissionModel permItem)
        {
            userName = HttpContext.User.Identity.Name;

            permItem.ModifiedBy = userName;
            permItem.ModifiedDate = DateTime.Now;
            permItem = await _permRepo.UpdatePermission(permItem);
            if (permItem == null)
            {
                _logger.LogError($"{userName} updates permission {permItem.PermissionID} failed");
                return Ok(new ResponseValue<PermissionModel> { IsSucess = false, ErrorMessage = "savechange_failed", ObjectValue = null });
            }

            _logger.LogInfo($"{userName} update permission {permItem.PermissionID} successfully");
            return Ok(new ResponseValue<PermissionModel> { IsSucess = true, ErrorMessage = "permission_update_sucess", ObjectValue = permItem });
        }

        [HttpPost]
        public async Task<IActionResult> Post(PermissionModel perm)
        {
            userName = HttpContext.User.Identity.Name;
            perm.CreatedBy = userName;
            perm.CreatedDate = DateTime.Now;

            bool isExist = await _permRepo.CheckExist(perm.PermissionID);

            if (isExist)
            {
                _logger.LogError($"{userName} create permission failed because permission has existed");
                return Ok(new ResponseValue<PermissionModel> { IsSucess = false, ErrorMessage = "permission_name_exist", ObjectValue = null });
            }

            perm = await _permRepo.CreatePermission(perm);
            if (perm == null)
            {
                _logger.LogError($"{userName} create permission failed");
                return Ok(new ResponseValue<PermissionModel> { IsSucess = false, ErrorMessage = "savechange_failed", ObjectValue = null });
            }

            _logger.LogInfo($"{userName} create permission {perm.PermissionID} successfully");
            return Ok(new ResponseValue<PermissionModel> { IsSucess = true, ErrorMessage = "permission_create_success", ObjectValue = perm });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string permID)
        {
            TblPermission permEntity = await _permRepo.GetByIdAsync(permID);
            if (permEntity == null)
            {
                return NotFound("The permission record couldn't be found.");
            }
            await _permRepo.DeleteAsync(permEntity);

            _logger.LogInfo($"{userName} delete permission {permID} successfully");
            return NoContent();
        }
    }
}
