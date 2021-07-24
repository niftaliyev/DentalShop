using DentalShop.Models.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DentalShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class LogoutController : Controller
    {
        private readonly SignInManager<AppUser> signInManager;

        public LogoutController(SignInManager<AppUser> signInManager)
        {
            this.signInManager = signInManager;
        }
        public IActionResult Index()
        {
            signInManager.SignOutAsync();
            return RedirectToAction("Index", "Products");
        }
    }
}
