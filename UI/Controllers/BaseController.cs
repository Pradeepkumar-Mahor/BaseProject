﻿using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace UI.Controllers
{
    public class BaseController : Controller
    {
        private readonly INotyfService _notyf;

        public IActionResult Index()
        {
            _notyf.Success("Success Notification");
            _notyf.Success("Success Notification that closes in 10 Seconds.", 3);

            _notyf.Error("Some Error Message", 4);
            _notyf.Warning("Some Error Message", 4);
            _notyf.Information("Information Notification - closes in 4 seconds.", 4);

            _notyf.Custom("Custom Notification - closes in 5 seconds.", 5, "whitesmoke", "fa fa-gear");
            _notyf.Custom("Custom Notification - closes in 5 seconds.", 10, "#B600FF", "fa fa-home");
            return View();
        }
    }
}