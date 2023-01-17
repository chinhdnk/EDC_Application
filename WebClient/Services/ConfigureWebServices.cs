using Application.ApiClient;
using Application.BusinessServices.Interfaces;
using Application.BusinessServices;
using Application.Services;
using System.Runtime.CompilerServices;

namespace WebClient.Services
{
    public static class ConfigureWebServices
    {
        public static IServiceCollection AddWebServices( this IServiceCollection services)
        {
            services.AddSingleton<ILocalStorageService, LocalStorageService>();
            services.AddScoped<IWebApiExecuter, WebApiExecuter>();

            services.AddTransient<IPermissionService, PermissionService>();

            return services;
        }
    }
}
