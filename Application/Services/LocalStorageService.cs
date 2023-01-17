using Microsoft.JSInterop;

namespace Application.Services
{
    public class LocalStorageService : ILocalStorageService
    {
        private const string SESSION_SET_ITEM = "sessionStorage.setItem";
        private const string SESSION_GET_ITEM = "sessionStorage.getItem";
        private const string SESSION_REMOVE_ITEM = "sessionStorage.removeItem";

        private const string LOCAL_SET_ITEM = "localStorage.setItem";
        private const string LOCAL_GET_ITEM = "localStorage.getItem";
        private const string LOCAL_REMOVE_ITEM = "localStorage.removeItem";

        private readonly IJSRuntime _iJSRuntime;
        public LocalStorageService(IJSRuntime iJSRuntime)
        {
            _iJSRuntime = iJSRuntime;
        }

        /// <summary>
        /// STORE ITEM VALUE IN SESSION
        /// </summary>

        public async Task SetSessionStorage(string key, string value)
        {
            await _iJSRuntime.InvokeVoidAsync(SESSION_SET_ITEM, key, value);
        }

        public async Task<string> GetSessionStorage(string key)
        {
            var value = await _iJSRuntime.InvokeAsync<string>(SESSION_GET_ITEM, key);
            if (value == null)
                return default;

            return value;
        }

        public async Task RemoveSessionStorage(string key)
        {
            await _iJSRuntime.InvokeVoidAsync(SESSION_REMOVE_ITEM, key);
        }

        /// <summary>
        /// STORE ITEM VALUE IN LOCAL STORAGE
        /// </summary>
        public async Task SetLocalStorage(string key, string value)
        {
            await _iJSRuntime.InvokeVoidAsync(LOCAL_SET_ITEM, key, value);
        }
        public async Task<string> GetLocalStorage(string key)

        {
            var value = await _iJSRuntime.InvokeAsync<string>(LOCAL_GET_ITEM, key);

            if (value == null)
                return default;

            return value;
        }
        public async Task RemoveLocalStorage(string key)
        {
            await _iJSRuntime.InvokeVoidAsync(LOCAL_REMOVE_ITEM, key);
        }
    }
}
