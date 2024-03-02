using CustomAuth.DataModels;

namespace CustomAuth.Services;

public interface IAuthService
{
    Task<bool> Register(RegisterModel registerModel);

    Task<UserModel> Login(LoginModel loginModel);
}