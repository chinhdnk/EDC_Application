using Application.Interfaces.IRepositories;
using Domain.Models;
using Infrastructure.Entities.AdminSystem;
using Microsoft.EntityFrameworkCore;
using BC = BCrypt.Net.BCrypt;

namespace Infrastructure.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly AdminDBContext _dbContext;
        public AccountRepository(AdminDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Task Accesslog(TblAccessLog accessLog)
        {
            throw new NotImplementedException();
        }
        public async Task<TblAccount> CheckUserIdentity(string username, string password)
        {
            TblAccount account = await _dbContext.TblAccounts.Where(x => x.Username == username).FirstOrDefaultAsync();

            if (account == null || (password != account.Password))
                return null;
            else
            { }
            return account;
        }

        public async Task UpdateAccount(TblAccount account)
        {
            _dbContext.Entry(account).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task<string> ChangePassword(ChangePasswordModel model)
        {
            try
            {
                TblAccount account = await CheckUserIdentity(model.Username, model.OldPassword);
                if (account == null)
                    //return "pw_incorrect";
                    return "the_old_password_is_not_correct";
                else
                {
                    //password_cannot_reused
                    string oldPassHash = BC.HashPassword(account.Salt + model.OldPassword);
                    List<TblPasswordHistory> ph = _dbContext.TblPasswordHistories.Where(t => t.Username == model.Username && t.CreatedDate > DateTime.Now.AddYears(-1)).ToList();
                    if (ph != null && ph.Count > 0)
                    {
                        foreach (var iph in ph)
                        {
                            if (BC.Verify(account.Salt + model.NewPassword, iph.Password))
                            {
                                return "password_cannot_reused";
                            }
                        }
                    }

                    //chang pass success reset_pw_success
                    string passwordHash = BC.HashPassword(account.Salt + model.NewPassword);
                    account.Password = passwordHash;
                    account.PasswordDate = DateTime.Now;
                    _dbContext.Entry(account).State = EntityState.Modified;
                    TblPasswordHistory phUpdate = new TblPasswordHistory()
                    {
                        Username = model.Username,
                        Password = passwordHash,
                        CreatedDate = DateTime.Now
                    };
                    _dbContext.TblPasswordHistories.Add(phUpdate);

                    await _dbContext.SaveChangesAsync();
                    return "reset_pw_success";
                }
            }
            catch (Exception)
            {
                return "system_error";
            }
        }
    }
}
