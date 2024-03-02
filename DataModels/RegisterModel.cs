using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CustomAuth.DataModels;

public class RegisterModel
{
    [Required(ErrorMessage = "Email Field is required ")]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [MinLength(5)]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    
    [Required]
    public string Role { get; set; }

    public override string ToString() => $"Email={Email}; Password={Password}";
}