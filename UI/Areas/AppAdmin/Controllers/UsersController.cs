using Domain.DataClass;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Utility;

namespace UI.Areas.AppAdmin.Controllers
{
    [Area("AppAdmin")]
    [Authorize(Roles = SD.SuperAdmin)]
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUsers> _userManager;

        public UsersController(UserManager<ApplicationUsers> userManager)
        {
            _userManager = userManager;
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
    }
}