using Microsoft.AspNetCore.Identity;

namespace NewAuth.Models;

public class User : IdentityUser
{
    public string[] Roles { get; set; }
}