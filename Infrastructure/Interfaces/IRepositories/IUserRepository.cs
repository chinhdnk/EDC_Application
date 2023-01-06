using Domain.Models;

namespace Infrastructure.Interfaces.IRepositories
{
    public interface IUserRepository
    {
        Task<bool> CheckExist(string userName);
        Task<bool> CheckExistEmail(string email);
        Task<UserModel> Create(UserModel item);
        PagingResponse<UserModel> GetUserList(PageParameters pageParameters, string userName, string fullName, string email);
    }
}