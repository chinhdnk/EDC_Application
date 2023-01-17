using Domain.Models;

namespace Application.ApiClient
{
    public interface IWebApiExecuter
    {
        Task<bool> InvokeDelete(string uri);
        Task<T> InvokeGet<T>(string uri);
        Task<T> InvokePost<T>(string uri, object obj);
        Task<IList<UploadResult>> InvokePostFile(string uri, MultipartFormDataContent files);
        Task<string> InvokePostReturnString<T>(string uri, T obj);
        Task<T> InvokePut<T>(string uri, object obj);
    }
}