using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ShoppingApi.Data;
using ShoppingApi.Model.Domain;
using ShoppingApi.Model.Dto;
using ShoppingApi.Repository.Interface;
using ShoppingApi.Utils;

namespace ShoppingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "AdminUser")]
    public class AccountController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly UserManager<AppUser> _userManager;
        private readonly ShoppingContext _context;
        
        public AccountController(IConfiguration config, UserManager<AppUser> userManager, ShoppingContext context)
        {
            _config = config;
            _userManager = userManager;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var userId = identity.FindFirst(Constants.Strings.JwtClaimIdentifiers.Id)?.Value;
            if (userId == null)
                return NotFound();

            var user = await _userManager.FindByIdAsync(userId);

            if (user != null)
                return Ok(user);
            return NotFound();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Post([FromBody]UserDto user)
        {
            if (ModelState.IsValid)
            {
                var appUser = new AppUser
                {
                    UserName = user.Username,
                    Email = user.Email,
                    Role = user.Role,
                };
                try
                {
                    var result = await _userManager.CreateAsync(appUser, user.Password);
                    if (!result.Succeeded)
                        return BadRequest(result.Errors);
                    await _context.SaveChangesAsync();

                    return Ok();
                }
                catch (Exception ex)
                {
                    return StatusCode(500, ex.Message);
                }

            }

            return BadRequest(ModelState);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UserDto user)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var userId = identity.FindFirst(Constants.Strings.JwtClaimIdentifiers.Id).Value;
            var appUser = await _userManager.FindByIdAsync(userId);

            if (appUser == null)
                return NotFound();
            appUser.UserName = user.Username;
            appUser.Email = user.Email;
            appUser.Role = user.Role;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] PasswordDto password)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var userId = identity.FindFirst(Constants.Strings.JwtClaimIdentifiers.Id).Value;
            var appUser = await _userManager.FindByIdAsync(userId);

            if (appUser == null)
                return NotFound();
            var result =
                await _userManager.ChangePasswordAsync(appUser, password.CurrentPassword, password.NewPassword);
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var userId = identity.FindFirst(Constants.Strings.JwtClaimIdentifiers.Id).Value;
            var appUser = await _userManager.FindByIdAsync(userId);

            if (appUser == null)
                return NotFound();

            var result = await _userManager.DeleteAsync(appUser);
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}