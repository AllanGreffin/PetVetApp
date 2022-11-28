using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PetVetApp.DTOs;
using PetVetApp.Helpers;
using PetVetApp.Services;

namespace PetVetApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly AuthService _authManager;

        public UserController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, AuthService authManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _authManager = authManager;
        }

        [HttpPost]
        [Route("Register")]
        public async Task RegisterUser([FromBody] UserDTO userDTO)
        {
            var user = new IdentityUser();
            user.UserName = userDTO.UserName;
            user.NormalizedUserName = userDTO.NormalizedUserName;
            user.Email = userDTO.Email;
            user.NormalizedEmail = userDTO.NormalizedEmail;
            user.EmailConfirmed = userDTO.EmailConfirmed;
            user.PasswordHash = userDTO.PasswordHash;
            user.SecurityStamp = userDTO.SecurityStamp;
            user.ConcurrencyStamp = userDTO.ConcurrencyStamp;
            user.PhoneNumber = userDTO.PhoneNumber;
            user.PhoneNumberConfirmed = userDTO.PhoneNumberConfirmed;
            user.TwoFactorEnabled = userDTO.TwoFactorEnabled;
            user.LockoutEnd = userDTO.LockoutEnd;
            user.LockoutEnabled = userDTO.LockoutEnabled;
            user.AccessFailedCount = userDTO.AccessFailedCount;

            var result = await _userManager.CreateAsync(user, userDTO.PasswordHash);
        }

        [HttpPost]
        [Route("Login")]
        public async Task<UserDTO> Login([FromBody] UserDTO userDTO)
        {
            var user = await _authManager.ValidateUser(userDTO);
            var result = user.ToUserDTO();
            if (user != null)
            {
                var token = await _authManager.CreateToken();
                //Response.Cookies.Append("Bearer", token, new CookieOptions { IsEssential = true, Secure = false, HttpOnly = false, SameSite = SameSiteMode.Lax });
                //Response.Headers.Add("Bearer", "token");
                result.Token = token;
                return result;
            }
            else
            {
                Response.StatusCode = 401;
                return null;
            }                
        }
    }
}
