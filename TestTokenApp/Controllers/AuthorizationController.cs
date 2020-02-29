using TestTokenApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace TestTokenApp.Controllers
{

    [Route("token")]
    [AllowAnonymous]
    public class TokenController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public TokenController(

        SignInManager<IdentityUser> signInManager,
        UserManager<IdentityUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]JwtLoginInputModel inputModel)
        {

            var user = await _userManager.FindByNameAsync(inputModel.Username);
            if (user == null)
            {
                return Unauthorized();
            }

            // Validate the username/password parameters and ensure the account is not locked out.
            var result = await _signInManager.CheckPasswordSignInAsync(user, inputModel.Password, lockoutOnFailure: true);
            if (!result.Succeeded)
            {
                return Unauthorized();
            }

            var token = new JwtTokenBuilder()
                                .AddSecurityKey(JwtSecurityKey.Create("fiver-secret-key"))
                                .AddSubject("james bond")
                                .AddIssuer("Fiver.Security.Bearer")
                                .AddAudience("Fiver.Security.Bearer")
                                .AddClaim("MembershipId", "111")
                                .AddExpiry(1)
                                .Build();

            //return Ok(token);
            return Ok(token.Value);

        }
    }
    public class AuthorizationController : Controller
    {
    }
}
