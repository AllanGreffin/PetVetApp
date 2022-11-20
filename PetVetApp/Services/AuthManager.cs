using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using PetVetApp.DTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PetVetApp.Services
{
    public class AuthManager
    {
        private readonly UserManager<IdentityUser> _userManager;
        private IdentityUser _apiUser;

        public AuthManager(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<string> CreateToken()
        {
            var signingCredential = GetSigningCredentials();
            var claims = await GetClaims();
            var tokenOptions = GenerateTokenOptions(signingCredential, claims);

            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }

        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredential, List<Claim> claims)
        {
            var issuer = "PetVetAppApi";
            var audience = "";
            
            var token = new JwtSecurityToken(
                issuer: issuer,
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: signingCredential
            );

            return token;
        }

        private async Task<List<Claim>> GetClaims()
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, _apiUser.UserName)
            };

            //var roles = await _userManager.GetRolesAsync(_apiUser);

            //foreach (var role in roles)
            //{
            //    claims.Add(new Claim(ClaimTypes.Role, role));
            //}

            return claims;
        }

        private SigningCredentials GetSigningCredentials()
        {
            var key = "PetVetAppApiSecretKey";
            var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        public async Task<bool> ValidateUser(UserDTO userDTO)
        {
            _apiUser = await _userManager.FindByNameAsync(userDTO.UserName);
            var result = _apiUser != null && await _userManager.CheckPasswordAsync(_apiUser, userDTO.PasswordHash);
            return result;
        }
    }
}
