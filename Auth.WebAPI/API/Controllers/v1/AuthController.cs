using Auth.Core.Application.DTOs;
using Auth.Core.Domain.IdentityEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Auth.WebAPI.API.Controllers.v1
{
    [Route("api/v1/auth")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public AuthController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<ApplicationRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            ApplicationUser user = new ApplicationUser
            {
                Email = registerDTO.Email
            };

			IdentityResult result = await _userManager.CreateAsync(user, registerDTO.Password);
			
			if (result.Succeeded)
			{
				await _signInManager.SignInAsync(user, isPersistent: false);

				return Ok(user);
			}
			else
			{
				string errorMessage = string.Join(" | ", result.Errors.Select(e => e.Description));
				return Problem(errorMessage);
			}
		}

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
			var result = await _signInManager.PasswordSignInAsync(loginDTO.Email, loginDTO.Password, isPersistent: false, lockoutOnFailure: false);
			
			if (result.Succeeded)
			{
				ApplicationUser? user = await _userManager.FindByEmailAsync(loginDTO.Email);

				if (user == null)
				{
					return NoContent();
				}

				return Ok(new { email = user.Email });
			}
			else
			{
				return Problem("Invalid email or password");
			}
		}

		[HttpGet("logout")]
		public async Task<IActionResult> Logout()
		{
			await _signInManager.SignOutAsync();
			
			return NoContent();
		}
	}
}
