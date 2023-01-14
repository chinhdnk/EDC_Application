using Domain.Models;

namespace Application.BusinessServices.Interfaces
{
    public interface IPermissionService
    {
        Task<PagingResponse<PermissionModel>> GetPermissions(int pageNumber);
    }
}