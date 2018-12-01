using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Pages.Account.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ShoppingApi.Model.Dto;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using ShoppingApi.Model.Domain;
using ShoppingApi.Utils;
using StandardLibrary.Extensions;

namespace ShoppingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly UserManager<AppUser> _userManager;
        private readonly ILogger _logger;

        public LoginController(IConfiguration config, UserManager<AppUser> userManager)
        {
            _config = config;
            _userManager = userManager;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Post([FromBody] LoginDto credentials)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var user = await Authenticate(credentials);
            if (user != null)
            {
                var token = BuildToken(user);
                return Ok(new{ token });
            }
            return Unauthorized();
        }

        
        private async Task<AppUser> Authenticate(LoginDto credentials)
        {
            if (credentials.Email.IsNullOrEmpty() || credentials.Password.IsNullOrEmpty())
                return null;
            var userToVerify = await _userManager.FindByEmailAsync(credentials.Email) ?? await _userManager.FindByNameAsync(credentials.Email);
            if (userToVerify == null)
                return null; 
            if (await _userManager.CheckPasswordAsync(userToVerify, credentials.Password))
                return userToVerify;
            return null;
        }

        private string BuildToken(AppUser user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(Constants.Strings.JwtClaimIdentifiers.Id, user.Id),
                new Claim(Constants.Strings.JwtClaimIdentifiers.Role, user.Role),
            };

            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddHours(4),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}