using DIY_API.DTOs.Authantication;

namespace DIY_API.Interfaces
{
    public interface IAuthantication
    {
        Task<string> SignUp(SignUpInputDTO input);
        Task<string> SignIn(SignInInputDTO input);
        Task<bool> SignOut(int userId);

    }
}
