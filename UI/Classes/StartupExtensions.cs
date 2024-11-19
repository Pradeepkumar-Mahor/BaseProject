using DataAccess.Repositories;
using Microsoft.AspNetCore.Authorization;
using UI.Models.IdenityUserAccess;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class StartupExtensions
    {
        public static IServiceCollection AddDataAccessService(this IServiceCollection services)
        {
            #region AuthService

            _ = services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
            _ = services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();

            #endregion AuthService

            #region PersonPro

            _ = services.AddTransient<IProductRepository, ProductRepository>();

            #endregion PersonPro

            return services;
        }
    }
}