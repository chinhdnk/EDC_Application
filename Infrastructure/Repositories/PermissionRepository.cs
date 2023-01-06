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
        public PermissionRepository(AdminDBContext dbContext) :base(dbContext)
        {
        }

        public async Task<PagingResponse<PermissionModel>> GetPermissionList(PageParameters pageParameters, string searchKey, bool status = false)
        {
            try
            {
                IEnumerable<PermissionModel> permList = await _dbContext.TblPermissions.Include(m => m.MenuNavigation)
                        .Select(u => new PermissionModel
                        {
                            PermissionID = u.PermId,
                            Title = u.Title,
                            MenuId = u.MenuNavigation.MenuId,
                            MenuName = u.MenuNavigation.Title,
                            Status = u.Status,
                            CreatedBy = u.CreatedBy,
                            CreatedDate = u.CreatedDate
                        }).ToListAsync();

                if (!string.IsNullOrWhiteSpace(searchKey))
                {
                    permList = permList.Where(x => x.Title.ToUpper().Contains(searchKey.ToUpper()));
                }

                PagedList<PermissionModel> list = PagedList<PermissionModel>.ToPagedList(permList, pageParameters.PageNumber, pageParameters.PageSize);

                PagingResponse<PermissionModel> pagingPerm = new PagingResponse<PermissionModel>
                {
                    Items = list,
                    PageData = list.PageData
                };

                return pagingPerm;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }           

        }

        public async Task<PermissionModel> UpdatePermission(PermissionModel permItem)
        {
            TblPermission permEntity = await _dbContext.TblPermissions.FindAsync(permItem.PermissionID);
            if (permEntity != null)
            {
                permItem.CreatedBy = permEntity.CreatedBy;
                permItem.CreatedDate = permEntity.CreatedDate;

                permEntity = Convert2Entity(permEntity, permItem);
                _dbContext.Entry(permEntity).State = EntityState.Modified;

                await _dbContext.SaveChangesAsync();
                return permItem;
            }
            else 
                return null;
        }
        public async Task<PermissionModel> CreatePermission(PermissionModel permission)
        {
            TblPermission permEntity = new TblPermission();
            permEntity = Convert2Entity(permEntity, permission);

            permEntity = await AddAsync(permEntity);

            permission.PermissionID = permEntity.PermId;
            return permission;
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

        private TblPermission Convert2Entity(TblPermission entity, PermissionModel permission)
        {
            entity.PermId = permission.PermissionID;
            entity.Title = permission.Title;
            entity.Status = permission.Status;
            entity.Menu = permission.MenuId;
            entity.CreatedBy = permission.CreatedBy;
            entity.CreatedDate = permission.CreatedDate;
            entity.ModifiedBy = permission.ModifiedBy;
            entity.ModifiedDate = permission.ModifiedDate;
            return entity;

        }
    }
}
