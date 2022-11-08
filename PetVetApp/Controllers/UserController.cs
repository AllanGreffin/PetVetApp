using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PetVetApp.DTOs;

namespace PetVetApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public UserController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
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

            return;
        }
    }
}
