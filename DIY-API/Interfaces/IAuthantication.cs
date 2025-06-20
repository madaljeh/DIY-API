using DIY_API.DTOs.Authantication;

namespace DIY_API.Interfaces
{
    public interface IAuthantication
    {
        Task<string> SignUp(SignUpInputDTO input);
        Task<string> SignIn(SignInInputDTO input);
        Task<string> Verification(VerificationInputDTO input);
        Task<bool> SendOTP(string email);

        Task<bool> ResetPersonPassword(ResetPersonPasswordInputDTO input);
        Task<bool> SignOut(int userId);

    }
}
