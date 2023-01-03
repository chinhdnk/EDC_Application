using Application.Interfaces.IRepositories;
using Domain.Models;
using Infrastructure.Entities.AdminSystem;
using Infrastructure.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class PermissionRepository : BaseRepository<TblPermission>, IPermissionRepository
    {
        //private readonly AdminDBContext _dbContext;
        public PermissionRepository(AdminDBContext dbContext) :base(dbContext)
        {
            //_dbContext = dbContext;
        }

        public async Task<bool> CheckExist(string perId)
        {
            TblPermission item = new TblPermission();

            item = await _dbContext.TblPermissions.FirstOrDefaultAsync(x => x.PermId == perId);

            return item != null;
        }     
        public IQueryable<string> GetPermOfUser(string userName)
        {
            var listPermUser = _dbContext.TblPermissions.Include(x => x.Usernames.Where(u => u.Username == userName)).Select(y => y.PermId);

            var listPermGroup = _dbContext.TblPermissions.Include(x => x.Groups).ThenInclude(y => y.Usernames.Where(u => u.Username == userName)).Select(z => z.PermId);

            var listPerm = listPermGroup.Concat(listPermUser);
            return listPerm.Distinct();
        }
    }
}
