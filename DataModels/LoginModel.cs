using System.ComponentModel.DataAnnotations;
namespace CustomAuth.DataModels;


public class LoginModel
{
    [Required]
    [EmailAddress(ErrorMessage = "Invalid Email Address")]
    public string Email { get; set; } = string.Empty; 
    
    [Required]
    public string Password { get; set; } = string.Empty;
}