using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ProductCQRS.Api.ViewModels;
using ProductCQRS.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProductCQRS.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthenticationController : ControllerBase
	{
		protected UserManager<User> UserManager { get; }
		protected RoleManager<IdentityRole> RoleManager { get; }
		protected IConfiguration Configuration { get; }

		public AuthenticationController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
		{
			UserManager = userManager;
			RoleManager = roleManager;
			Configuration = configuration;
		}

		[HttpPost]
		[Route("login")]
		public async Task<IActionResult> Login([FromBody] LoginViewModel loginViewModel)
		{
			var user = await UserManager.FindByNameAsync(loginViewModel.Username);

			if (user != null && await UserManager.CheckPasswordAsync(user, loginViewModel.Password))
			{
				var userRoles = await UserManager.GetRolesAsync(user);

				var authClaims = new List<Claim>
				{
					new Claim(ClaimTypes.Name, user.UserName),
					new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
				};

				foreach (var userRole in userRoles)
				{
					authClaims.Add(new Claim(ClaimTypes.Role, userRole));
				}

				var token = GetToken(authClaims);

				return Ok(new
				{
					token = new JwtSecurityTokenHandler().WriteToken(token),
					expiration = token.ValidTo
				});
			}

			return Unauthorized();
		}

		[HttpPost]
		[Route("register")]
		public async Task<IActionResult> Register([FromBody] RegisterViewModel registerViewModel)
		{
			var userExists = await UserManager.FindByNameAsync(registerViewModel.Username);

			if (userExists != null)
			{
				return StatusCode(StatusCodes.Status500InternalServerError);
			}

			User user = new()
			{
				Email = registerViewModel.Email,
				SecurityStamp = Guid.NewGuid().ToString(),
				UserName = registerViewModel.Username
			};

			var result = await UserManager.CreateAsync(user, registerViewModel.Password);

			if (!result.Succeeded)
			{
				return StatusCode(StatusCodes.Status500InternalServerError);
			}

			if (!await RoleManager.RoleExistsAsync(UserRoles.Admin))
			{
				await RoleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
			}

			if (!await RoleManager.RoleExistsAsync(UserRoles.User))
			{
				await RoleManager.CreateAsync(new IdentityRole(UserRoles.User));
			}

			if (await RoleManager.RoleExistsAsync(UserRoles.Admin))
			{
				await UserManager.AddToRoleAsync(user, UserRoles.Admin);
			}

			if (await RoleManager.RoleExistsAsync(UserRoles.User))
			{
				await UserManager.AddToRoleAsync(user, UserRoles.User);
			}

			return Ok();
		}

		private JwtSecurityToken GetToken(List<Claim> authClaims)
		{
			var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Secret"]));

			var token = new JwtSecurityToken(
				issuer: Configuration["JWT:ValidIssuer"],
				audience: Configuration["JWT:ValidAudience"],
				expires: DateTime.Now.AddHours(1),
				claims: authClaims,
				signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
				);

			return token;
		}
	}
}
