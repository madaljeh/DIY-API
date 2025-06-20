using System;
using System.Collections.Generic;

namespace DIY_API.Models;

public partial class User
{
    public int UserId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    public string? ProfileImage { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? CreationDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public bool IsActive { get; set; }

    public bool IsVerified { get; set; }

    public int RoleId { get; set; }

    public string? Otp { get; set; }

    public DateTime? ExpireOtp { get; set; }

    public DateTime? LastLoginTime { get; set; }

    public bool IsLogedIn { get; set; }

    public virtual ICollection<Rating> Ratings { get; set; } = new List<Rating>();

    public virtual UserRole Role { get; set; } = null!;

    public virtual ICollection<UserChallenge> UserChallenges { get; set; } = new List<UserChallenge>();
}
