using DataAccess.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection.Repositories;
using UI.Models.IdenityUserAccess;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class StartupExtensions
    {
        public static IServiceCollection AddDataAccessService(this IServiceCollection services)
        {
            #region AuthService

            services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
            services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();

            #endregion AuthService

            #region PersonPro

            services.AddTransient<IProductRepository, ProductRepository>();

            #endregion PersonPro

            return services;
        }
    }
}