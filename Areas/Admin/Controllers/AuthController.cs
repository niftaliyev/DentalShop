using DentalShop.Models.Identity;
using DentalShop.Areas.Admin.Model;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DentalShop.ViewModels;
using DentalShop.Models.DTO;
using DentalShop.Models;
using DentalShop.Services;
using Microsoft.AspNetCore.Authorization;

namespace DentalShop.Controllers
{
    [Area("Admin")]
 
        public class AuthController : Controller
        {
            private readonly DentalShopDbContext context;
            private readonly UserManager<AppUser> userManager;
            private readonly RoleManager<IdentityRole> roleManager;
            private readonly TokenGenerator tokenGenerator;

            public AuthController(
                DentalShopDbContext context,
                UserManager<AppUser> userManager,
                RoleManager<IdentityRole> roleManager,
                TokenGenerator tokenGenerator)
            {
                this.context = context;
                this.userManager = userManager;
                this.roleManager = roleManager;
                this.tokenGenerator = tokenGenerator;
            }



            [HttpGet]
            
            public IActionResult Login()
            {
                if (User.Identity.IsAuthenticated)
                {
                    if (Request.Cookies.ContainsKey("x"))
                    {
                        return RedirectToAction("Index", "Products");
                    }
                return View();

            }

            else
                return View();
            }


            [HttpPost]
            public async Task<IActionResult> Login(AccountCredentialsDTO credentials)
            {
                var user = await userManager.FindByNameAsync(credentials.Email);

                if (user == null)
                    return Unauthorized();
                if (!await userManager.CheckPasswordAsync(user, credentials.Password))
                    return Unauthorized();

                var accessToken = tokenGenerator.GenerateAccessToken(user);



                Response.Cookies.Append("x", accessToken);
                return RedirectToAction("Index","Products");
            }


            /// <summary>
            /// /////////////////////////////
            /// </summary>


            [HttpGet]
            public IActionResult Logout()
            {
                HttpContext.Response.Cookies.Delete("x");
                return RedirectToAction("Login", "Auth");
            }



            [HttpGet]
            public IActionResult AccessDenied(string returnUrl)
            {
                return View();
            }



        }
    }

