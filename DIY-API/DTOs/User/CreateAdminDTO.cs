namespace DIY_API.DTOs.User
{
    public class CreateAdminDTO
    {
        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Username { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string? PhoneNumber { get; set; }

        public string? ProfileImage { get; set; }
        public int Age { get; set; }

        public string Gender { get; set; } = null!;


        public DateTime? CreationDate { get; set; }

        public bool IsActive { get; set; }

        public int RoleId { get; set; }
    }

}
