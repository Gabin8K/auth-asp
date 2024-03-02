using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CustomAuth.DataModels;

public class UserModel
{
    public string Email { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
    public List<string> Roles { get; set; } = new();
}