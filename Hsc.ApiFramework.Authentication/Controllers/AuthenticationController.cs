using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Hsc.ApiFramework.Configuration;
using Hsc.ApiFramework.Configuration.Logic;
using Hsc.ApiFramework.Enums;
using Hsc.ApiFramework.Interfaces;
using Hsc.ApiFramework.Models.Identity.Implementation;
using Hsc.ApiFramework.Models.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Hsc.ApiFramework.Core.Controllers
{
    /// <summary>
    /// Controller to handle authentication
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {

        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        /// <summary>
        /// Controller to handle authentication
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="roleManager"></param>
        public AuthenticationController(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        /// <summary>
        /// Endpoint to get login JWT token
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);

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

        /// <summary>
        /// Endpoint to register a new user
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new RequestResponse { Status = "Error", Message = "User already exists!" });

            IdentityUser user = new()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new RequestResponse { Status = "Error", Message = result.Errors.FirstOrDefault()?.Description });

            await _userManager.AddToRoleAsync(user, HscUserRoles.User.ToString());

            return Ok(user);
        }

        /// <summary>
        /// Register admin user
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("register-admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterModel model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new RequestResponse { Status = "Error", Message = "User already exists!" });

            IdentityUser user = new()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new RequestResponse { Status = "Error", Message = result.Errors.FirstOrDefault()?.Description });

            await _userManager.AddToRoleAsync(user, HscUserRoles.Admin.ToString());
            await _userManager.AddToRoleAsync(user, HscUserRoles.Moderator.ToString());
            await _userManager.AddToRoleAsync(user, HscUserRoles.User.ToString());

            return Ok(user);
        }

        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(HscEnvironmentConfiguration.GetSetting(HscSetting.HSC_AUTH_JWT_SECRET)!));

            var token = new JwtSecurityToken(
                issuer: HscEnvironmentConfiguration.GetSetting(HscSetting.HSC_AUTH_JWT_ISSUER),
                audience: HscEnvironmentConfiguration.GetSetting(HscSetting.HSC_AUTH_JWT_AUDIENCE),
                expires: DateTime.Now.AddHours(HscEnvironmentConfiguration.GetDoubleSetting(HscSetting.HSC_TOKEN_DURATION) ?? 3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }
    }
}
