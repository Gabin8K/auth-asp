using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CustomAuth.Data.Auth;
using CustomAuth.DataModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace CustomAuth.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly ILogger<AuthService> _logger;
    private readonly IConfiguration _configuration;

    public AuthService(UserManager<User> userManager, RoleManager<IdentityRole> roleManager,
        ILogger<AuthService> logger, IConfiguration configuration)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _logger = logger;
        _configuration = configuration;
    }

    public async Task<bool> Register(RegisterModel registerModel)
    {
        var existUser = await _userManager.FindByEmailAsync(registerModel.Email);
        var existRole = await _roleManager.RoleExistsAsync(registerModel.Role);
        if (existUser != null || !existRole)
        {
            return false;
        }

        User user = new User
        {
            Email = registerModel.Email,
            UserName = registerModel.Email,
            SecurityStamp = Guid.NewGuid().ToString(),
        };
        var createResult = await _userManager.CreateAsync(user, registerModel.Password);
        await _userManager.AddToRoleAsync(user, registerModel.Role);
        _logger.Log(LogLevel.Warning, registerModel.ToString());
        return createResult.Succeeded;
    }

    public async Task<UserModel> Login(LoginModel loginModel)
    {
        var user = await _userManager.FindByEmailAsync(loginModel.Email);
        var passwordMatch = await _userManager.CheckPasswordAsync(user, loginModel.Password);
        if (!passwordMatch)
        {
            return null;
        }

        var roles = (await _userManager.GetRolesAsync(user)).ToArray();
        var authClaim = new List<Claim>
        {
            new (ClaimTypes.Name, user?.Email ?? ""),
            new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        foreach (var role in roles)
        {
            authClaim.Add(new Claim(ClaimTypes.Role, role));
        }

        var token = GenerateToken(authClaim);
        var userModel = new UserModel
        {
            Email = user.Email,
            Token = token,
            Roles = roles.ToList()
        };
        return userModel;
    }


    private string GenerateToken(List<Claim> claims)
    {
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"] ?? ""));
        var tokenDescription = new SecurityTokenDescriptor
        {
            Issuer = _configuration["JWT:ValidIssuer"],
            Audience = _configuration["JWT:ValidAudience"],
            Expires = DateTime.UtcNow.AddHours(3),
            SigningCredentials = new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256),
            Subject = new ClaimsIdentity(claims)
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescription);
        return tokenHandler.WriteToken(token);
    }
}