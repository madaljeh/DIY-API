namespace DIY_API.DTOs.User
{
    public class UpdateUserInfoDTO
    {
        public string? FirstName { get; set; } 

        public string? LastName { get; set; } 

        public string? Username { get; set; } 

        public string? Email { get; set; } 

        public string? PhoneNumber { get; set; }
        public int Age { get; set; }

        public string Gender { get; set; } = null!;

        public string? ProfileImage { get; set; }
    }
}
