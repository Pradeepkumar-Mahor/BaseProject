using DataAccess.Context;
using DataAccess.Core.Interface;
using DataAccess.Core.Repository;
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

            #region DbContext

            _ = services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            #region CoreTables

            services.AddScoped<ICategoryRepository, CategoryRepository>();

            #endregion CoreTables

            #endregion DbContext

            #region SqlUsingDapper

            #region PersonPro

            _ = services.AddTransient<IProductRepository, ProductRepository>();

            #endregion PersonPro

            #endregion SqlUsingDapper

            return services;
        }
    }
}