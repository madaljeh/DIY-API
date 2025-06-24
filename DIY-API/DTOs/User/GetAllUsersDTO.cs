namespace DIY_API.DTOs.User
{
    public class GetAllUsersDTO
    {
        public int UserId { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Username { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string Email { get; set; } = null!;
        public int Age { get; set; }

        public string Gender { get; set; } = null!;

        public string? PhoneNumber { get; set; }

        public string? ProfileImage { get; set; }
        public bool IsActive { get; set; }
        public int RoleId { get; set; }
        public DateTime? CreationDate { get; set; }

    }
}
