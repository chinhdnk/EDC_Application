using Application.Services;
using Domain.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Application.ApiClient
{
    public class WebApiExecuter : IWebApiExecuter
    {
        private readonly HttpClient _httpClient;
        public WebApiExecuter(HttpClient httpClient, ILocalStorageService localStorageService)
        {
            _httpClient = httpClient;
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<T> InvokeGet<T>(string uri)
        {
            var response = await _httpClient.GetAsync(uri);

            if (!response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                throw new ApplicationException(result);
            }

            var content = await response.Content.ReadFromJsonAsync<T>();
            return content;
        }

        public async Task<T> InvokePost<T>(string uri, object obj)
        {
            var response = await _httpClient.PostAsJsonAsync(uri, obj);

            if (!response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                throw new ApplicationException(result);
            }

            var content = await response.Content.ReadFromJsonAsync<T>();

            return content;
        }

        public async Task<string> InvokePostReturnString<T>(string uri, T obj)
        {
            var response = await _httpClient.PostAsJsonAsync(uri, obj);
            var result = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(result);
            }
            return result;

        }

        public async Task<T> InvokePut<T>(string uri, object obj)
        {
            var response = await _httpClient.PutAsJsonAsync(uri, obj);

            if (!response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                throw new ApplicationException(result);
            }
            var content = await response.Content.ReadFromJsonAsync<T>();
            return content;
        }

        public async Task<bool> InvokeDelete(string uri)
        {
            var response = await _httpClient.DeleteAsync(uri);
            if (!response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                throw new ApplicationException(result);
            }
            var content = await response.Content.ReadFromJsonAsync<bool>();
            return content;
        }

        public async Task<IList<UploadResult>> InvokePostFile(string uri, MultipartFormDataContent files)
        {
            var response = await _httpClient.PostAsync(uri, files);
            if (!response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                throw new ApplicationException(result);
            }
            var uploadResult = await response.Content.ReadFromJsonAsync<IList<UploadResult>>();
            return uploadResult;
        }
    }
}
