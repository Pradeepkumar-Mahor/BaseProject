using Domain.Core;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Domain
{
    public static class DefaultUsers
    {
        public static async Task SeedBasicUserAsync(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            IdentityUser defaultUser = new()
            {
                UserName = "basicuser@gmail.com",
                Email = "basicuser@gmail.com",
                EmailConfirmed = true
            };
            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                IdentityUser? user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    _ = await userManager.CreateAsync(defaultUser, "123Pa$$word!");
                    _ = await userManager.AddToRoleAsync(defaultUser, Roles.Basic.ToString());
                }
            }
        }

        public static async Task SeedSuperAdminAsync(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            IdentityUser defaultUser = new()
            {
                UserName = "SuperAdmin@gmail.com",
                Email = "SuperAdmin@gmail.com",
                EmailConfirmed = true
            };
            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                IdentityUser? user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    _ = await userManager.CreateAsync(defaultUser, "123Pa$$word!");
                    _ = await userManager.AddToRoleAsync(defaultUser, Roles.Basic.ToString());
                    _ = await userManager.AddToRoleAsync(defaultUser, Roles.Admin.ToString());
                    _ = await userManager.AddToRoleAsync(defaultUser, Roles.SuperAdmin.ToString());
                }
                await roleManager.SeedClaimsForSuperAdmin();
            }
        }

        private static async Task SeedClaimsForSuperAdmin(this RoleManager<IdentityRole> roleManager)
        {
            IdentityRole? adminRole = await roleManager.FindByNameAsync("SuperAdmin");
            await roleManager.AddPermissionClaim(role: adminRole, "Users");
            await roleManager.AddPermissionClaim(role: adminRole, "Roles");
            await roleManager.AddPermissionClaim(role: adminRole, "UserRoles");
        }

        public static async Task AddPermissionClaim(this RoleManager<IdentityRole> roleManager, IdentityRole role, string module)
        {
            IList<Claim> allClaims = await roleManager.GetClaimsAsync(role);
            List<string> allPermissions = Permissions.GeneratePermissionsForModule(module);
            foreach (string permission in allPermissions)
            {
                if (!allClaims.Any(a => a.Type == "Permission" && a.Value == permission))
                {
                    _ = await roleManager.AddClaimAsync(role, new Claim("Permission", permission));
                }
            }
        }
    }
}