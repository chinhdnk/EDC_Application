namespace Infrastructure.Services.Interfaces
{
    public interface ILocalStorageService
    {
        Task<string> GetLocalStorage(string key);
        Task<string> GetSessionStorage(string key);
        Task RemoveLocalStorage(string key);
        Task RemoveSessionStorage(string key);
        Task SetLocalStorage(string key, string value);
        Task SetSessionStorage(string key, string value);
    }
}