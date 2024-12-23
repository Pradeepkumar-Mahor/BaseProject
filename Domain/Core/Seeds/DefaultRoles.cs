﻿using Domain.DataClass;
using Microsoft.AspNetCore.Identity;

namespace Domain
{
    public static class DefaultRoles
    {
        public static async Task SeedAsync(UserManager<ApplicationUsers> userManager, RoleManager<IdentityRole> roleManager)
        {
            _ = await roleManager.CreateAsync(new IdentityRole(Roles.SuperAdmin.ToString()));
            _ = await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
            _ = await roleManager.CreateAsync(new IdentityRole(Roles.Basic.ToString()));
        }
    }
}