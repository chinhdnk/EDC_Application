using Domain.Models;
using Domain.Shared;
using Infrastructure.Entities.AdminSystem;
using Infrastructure.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        protected readonly AdminDBContext _dbContext;
        public UserRepository(AdminDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public PagingResponse<UserModel> GetUserList(PageParameters pageParameters, string userName, string fullName, string email)
        {
            try
            {
                IEnumerable<UserModel> userList = (from u in _dbContext.TblUsers
                                                   join a in _dbContext.TblAccounts on u.Username equals a.Username
                                                   select new UserModel
                                                   {
                                                       Username = u.Username,
                                                       FullName = a.FullName,
                                                       Email = a.Email,
                                                       MPhone = u.MPhone,
                                                       OPhone = u.OPhone,
                                                       ProfileImage = u.ProfileImage,
                                                       ESignature = u.ESignature,
                                                       City = u.City,
                                                       Country = u.Country,
                                                       Institution = u.Institution,
                                                       Status = a.Status,
                                                       CreatedBy = u.CreatedBy,
                                                       CreatedDate = u.CreatedDate,
                                                       ExpiredDate = a != null ? a.ExpiredDate : null,
                                                   });

                if (!string.IsNullOrEmpty(userName))
                    userList = userList.Where(u => u.Username == userName);

                if (!string.IsNullOrEmpty(fullName))
                    userList = userList.Where(u => u.FullName == fullName);

                if (!string.IsNullOrEmpty(email))
                    userList = userList.Where(u => u.Email == email);

                PagedList<UserModel> list = PagedList<UserModel>.ToPagedList(userList, pageParameters.PageNumber, pageParameters.PageSize);

                PagingResponse<UserModel> pagingUser = new PagingResponse<UserModel>
                {
                    Items = list,
                    PageData = list.PageData
                };

                return pagingUser;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

        }

        public async Task<bool> CheckExist(string userName)
        {
            TblUser item = new TblUser();
            item = await _dbContext.TblUsers.FirstOrDefaultAsync(x => x.Username.ToUpper() == userName.ToUpper());
            return item != null;
        }
        public async Task<bool> CheckExistEmail(string email)
        {
            TblAccount item = new TblAccount();
            item = await _dbContext.TblAccounts.FirstOrDefaultAsync(x => x.Email.ToUpper() == email.ToUpper());
            return item != null;
        }

        public async Task<UserModel> Create(UserModel item)
        {
            TblUser entity = new TblUser();
            entity = Convert2Entity(entity, item);
            //save to tblUsers
            _dbContext.TblUsers.Add(entity);

            //save to TblAccount
            TblAccount entityAc = new TblAccount();
            entityAc = Convert2AcountEntity(entityAc, item);
            _dbContext.TblAccounts.Add(entityAc);
            await _dbContext.SaveChangesAsync();
            item.Username = entity.Username;
            return item;
        }

        private TblUser Convert2Entity(TblUser entity, UserModel item)
        {
            entity.Username = item.Username;
            entity.MPhone = item.MPhone;
            entity.OPhone = item.OPhone;
            entity.ESignature = item.ESignature;
            entity.ProfileImage = item.ProfileImage;
            entity.City = item.City;
            entity.Country = item.Country;
            entity.Institution = item.Institution;
            entity.CreatedBy = item.CreatedBy;
            entity.CreatedDate = item.CreatedDate;
            return entity;
        }

        private TblAccount Convert2AcountEntity(TblAccount entity, UserModel item)
        {
            entity.Username = item.Username;
            entity.FullName = item.FullName;
            entity.Email = item.Email;
            entity.Status = item.Status;
            entity.CreatedBy = item.CreatedBy;
            entity.CreatedDate = item.CreatedDate;
            entity.ExpiredDate = item.ExpiredDate;
            entity.Salt = SharedFunction.RandomString(11);
            return entity;
        }
    }
}
