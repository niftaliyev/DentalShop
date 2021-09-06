using DentalShop.Models;
using DentalShop.Models.DTO;
using DentalShop.Models.Identity;
using DentalShop.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DentalShop.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class AccountController : ControllerBase
    {
        private readonly DentalShopDbContext context;
        private readonly UserManager<AppUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly TokenGenerator tokenGenerator;

        public AccountController(
            DentalShopDbContext context,
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<AppUser> signInManager,
            TokenGenerator tokenGenerator)
        {
            this.context = context;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.signInManager = signInManager;
            this.tokenGenerator = tokenGenerator;
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthResponseDTO>> Login(AccountLoginDTO credentials)
        {
            var user = await userManager.FindByNameAsync(credentials.Email);

            if (user == null)
                return Unauthorized();
            if (!await userManager.CheckPasswordAsync(user, credentials.Password))
                return Unauthorized();

            var test = context.UserRoles.Where(x => x.UserId == user.Id).FirstOrDefault().RoleId;
            var role = await roleManager.FindByIdAsync(test);
            var accessToken = tokenGenerator.GenerateAccessToken(user, role);


            var response = new AuthResponseDTO
            {
                AccessToken = accessToken,
                UserId = user.Id,
                Username = user.UserName

            };
            return response;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(AccountCredentialsDTO credentials)
        {
            var user = new AppUser
            {
                Email = credentials.Email,
                Name = credentials.Name,
                UserName = credentials.Name,
                PhoneNumber = credentials.Phone
            };

            var result = await userManager.CreateAsync(user, credentials.Password);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok();
        }
    }
}
