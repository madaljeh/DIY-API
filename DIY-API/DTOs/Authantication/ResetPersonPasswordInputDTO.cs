namespace DIY_API.DTOs.Authantication
{
    public class ResetPersonPasswordInputDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Otp { get; set; }
    }
}
