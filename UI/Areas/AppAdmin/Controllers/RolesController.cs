﻿using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UI.Controllers;
using Utility;

namespace UI.Areas.AppAdmin.Controllers
{
    [Area("AppAdmin")]
    [Authorize(Roles = SD.SuperAdmin)]
    public class RolesController : BaseController
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly INotyfService _notyf;

        public RolesController(RoleManager<IdentityRole> roleManager, INotyfService notyf)
        {
            _roleManager = roleManager;
            _notyf = notyf;
        }

        public async Task<IActionResult> Index()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return View(roles);
        }

        [HttpPost]
        public async Task<IActionResult> AddRole(string roleName)
        {
            if (roleName != null)
            {
                await _roleManager.CreateAsync(new IdentityRole(roleName.Trim()));
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(IdentityRole role)
        {
            if (role.Name is null)
            {
                _notyf.Error("Error : Role Name not found", 4);
                return View(role);
            }
            if (!_roleManager.RoleExistsAsync(roleName: role.Name).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(role.Name)).GetAwaiter().GetResult();
            }
            return RedirectToAction("Index");
        }
    }
}