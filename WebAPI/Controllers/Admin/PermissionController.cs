using Domain.Models;
using Infrastructure.Entities.AdminSystem;
using Infrastructure.Interfaces.IRepositories;
using Infrastructure.Repositories;
using Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.Admin
{
    [ApiController]
    [Route("api/admin/[controller]")]
    public class PermissionController : ControllerBase
    {
        private readonly IPermissionRepository _permRepo;
        private readonly ILoggerManager _logger;
        public PermissionController(IPermissionRepository permRepo, ILoggerManager logger)
        {
            _permRepo = permRepo;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            IReadOnlyList<TblPermission> permLists = await _permRepo.GetAllAsync();
            return Ok(permLists);
        }
    }
}
