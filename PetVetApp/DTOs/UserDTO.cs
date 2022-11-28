using Microsoft.AspNetCore.Identity;

namespace PetVetApp.DTOs
{
    public class UserDTO : IdentityUser
    {
        public string? Token { get; set; }
    }
}
