using Microsoft.AspNetCore.Identity;
using PetVetApp.DTOs;

namespace PetVetApp.Helpers
{
    public static class IdentityUserExtension
    {
        public static UserDTO ToUserDTO(this IdentityUser user)
        {
            return new UserDTO
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
            };
        }
    }
}
