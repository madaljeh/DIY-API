using System;
using System.Collections.Generic;

namespace DIY_API.Models;

public partial class Challenge
{
    public int ChallengesId { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public string? MediaUrl { get; set; }

    public string? Level { get; set; }

    public int? EstimatedDuration { get; set; }

    public int CategoryId { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreationDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual ICollection<Rating> Ratings { get; set; } = new List<Rating>();

    public virtual ICollection<UserChallenge> UserChallenges { get; set; } = new List<UserChallenge>();
}
