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
            services.AddSingleton<ILoggerManager, LoggerManager>();
            services.AddTransient<IFileService, FileService>();

            services.AddTransient<IJwtTokenManager, JwtTokenManager>();
            services.AddTransient<IAccountRepository, AccountRepository>();


            services.AddTransient<IPermissionRepository, PermissionRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            return services;
        }
    }
}
