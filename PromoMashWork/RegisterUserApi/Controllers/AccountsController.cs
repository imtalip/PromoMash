namespace RegisterUserApi.Controllers
{
    using IdentityModel;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using RegisterUserApi.Models;
    using System.Security.Claims;

    [Route("api/accounts")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;

        public AccountsController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost("Registration")]
        public async Task<IActionResult> RegisterUser(
            [FromBody] UserForRegistrationDto userForRegistration)
        {
            if (userForRegistration == null || !ModelState.IsValid)
                return BadRequest();

            var userExist = _userManager.FindByNameAsync(userForRegistration.Name).Result;
            if (userExist == null)
            {
                var newUser = new IdentityUser
                {
                    UserName = userForRegistration.Name,
                    Email = userForRegistration.Email,
                    EmailConfirmed = true
                };
                var result = await _userManager.CreateAsync(newUser, userForRegistration.Password);

                if (!result.Succeeded)
                {
                    var errors = result.Errors.Select(e => e.Description);

                    return BadRequest(new RegistrationResponseDto { Errors = errors });
                }

                result = await _userManager.AddClaimsAsync(
                        newUser,
                        new Claim[]
                        {
                            new Claim(JwtClaimTypes.Name, userForRegistration.Name),
                            new Claim(JwtClaimTypes.GivenName, userForRegistration.Name),
                            new Claim(JwtClaimTypes.FamilyName, userForRegistration.Name),
                            new Claim("location", "somewhere")
                        }
                    );

                if (!result.Succeeded)
                {
                    var errors = result.Errors.Select(e => e.Description);

                    return BadRequest(new RegistrationResponseDto { Errors = errors });
                }
            }
            else
            {
                return BadRequest(new RegistrationResponseDto { Errors = new[] { "Пользователь уже доабвлен" } });
            }

            return StatusCode(201);
        }
    }
}
