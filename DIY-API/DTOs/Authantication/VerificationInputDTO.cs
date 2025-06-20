namespace DIY_API.DTOs.Authantication
{
    public class VerificationInputDTO
    {
        public string Email { get; set; }

        public string Otp { get; set; }

        public bool IsLogedIn { get; set; }
    }
}
