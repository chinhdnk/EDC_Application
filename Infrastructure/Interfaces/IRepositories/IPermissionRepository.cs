using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Application.Interfaces.IRepositories;
using Infrastructure.Entities.AdminSystem;

namespace Infrastructure.Interfaces.IRepositories
{
    public interface IPermissionRepository: IBaseRepository<TblPermission>
    {
        IQueryable<string> GetPermOfUser(string userName);
        Task<bool> CheckExist(string perId);
    }
}
