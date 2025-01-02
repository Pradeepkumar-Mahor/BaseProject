using Domain.DataClass;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Text.Encodings.Web;
using UI.Models;
using Utility;

namespace UI.Areas.AppAdmin.Controllers
{
    [Area("AppAdmin")]
    [Authorize(Roles = SD.SuperAdmin)]
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUsers> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUsers> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly UrlEncoder _urlEncoder;

        public UsersController(UserManager<ApplicationUsers> userManager, SignInManager<ApplicationUsers> signInManager,
            IEmailSender emailSender, UrlEncoder urlEncoder, RoleManager<IdentityRole> roleManager)
        {
            _emailSender = emailSender;
            _urlEncoder = urlEncoder;
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var currentUser = await _userManager.GetUserAsync(HttpContext.User);
                var allUsersExceptCurrentUser =
                        await _userManager.Users.Where(a => a.Id != currentUser.Id).ToListAsync();
                return View(allUsersExceptCurrentUser);
            }
            catch (Exception xe)
            {
                throw;
            }
        }

        public async Task<IActionResult> Create(string returnurl = null)
        {
            if (!_roleManager.RoleExistsAsync(SD.Admin).GetAwaiter().GetResult())
            {
                await _roleManager.CreateAsync(new IdentityRole(SD.Admin));
                await _roleManager.CreateAsync(new IdentityRole(SD.Basic));
            }

            ViewData["ReturnUrl"] = returnurl;
            RegisterViewModel registerViewModel = new()
            {
                RoleList = _roleManager.Roles.Select(x => x.Name).Select(i => new SelectListItem
                {
                    Text = i,
                    Value = i
                })
            };
            return View(registerViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RegisterViewModel model, string returnurl = null)
        {
            ViewData["ReturnUrl"] = returnurl;
            returnurl = returnurl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                var user = new ApplicationUsers
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.FirstName,
                    MidName = model.MidName,
                    CreateOnDate = model.CreateOnDate,
                    PanNo = model.PanNo,
                    AadhaarNo = model.AadhaarNo,
                    Gender = model.Gender,
                    Photo = model.Photo,
                    Dob = model.Dob,
                };

                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    if (model.RoleSelected != null)
                    {
                        await _userManager.AddToRoleAsync(user, model.RoleSelected);
                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(user, SD.Basic);
                    }

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackurl = Url.Action("ConfirmEmail", "Account", new
                    {
                        userid = user.Id,
                        code
                    }, protocol: HttpContext.Request.Scheme);

                    await _emailSender.SendEmailAsync(model.Email, "Confirm Email - Identity Manager",
                                           $"Please confirm your email by clicking here: <a href='{callbackurl}'>link</a>");

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return LocalRedirect(returnurl);
                }

                AddErrors(result);
            }
            model.RoleList = _roleManager.Roles.Select(x => x.Name).Select(i => new SelectListItem
            {
                Text = i,
                Value = i
            });
            return View(model);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
    }
}