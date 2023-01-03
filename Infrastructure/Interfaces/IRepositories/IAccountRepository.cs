using Domain.Models;
using Infrastructure.Entities.AdminSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IRepositories
{
    public interface IAccountRepository
    {
        Task<string> ChangePassword(ChangePasswordModel model);
        Task Accesslog(TblAccessLog accessLog);
        Task<TblAccount> CheckUserIdentity(string username, string password);
        Task UpdateAccount(TblAccount account);
    }
}
