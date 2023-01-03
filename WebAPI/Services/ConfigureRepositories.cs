using Application.Interfaces.IRepositories;
using Infrastructure.Repositories;
using Infrastructure.Services.Interfaces;
using Infrastructure.Services;
using WebAPI.Authentication;
using Infrastructure.Interfaces.IRepositories;
using Infrastructure.Entities.AdminSystem;

namespace WebAPI.Services
{
    public static class ConfigureRepositories
    {
        public static IServiceCollection AddRepositoryServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

            services.AddTransient<IJwtTokenManager, JwtTokenManager>();
            services.AddTransient<IAccountRepository, AccountRepository>();
            services.AddSingleton<ILoggerManager, LoggerManager>();

            services.AddTransient<IPermissionRepository, PermissionRepository>();
            return services;
        }
    }
}
