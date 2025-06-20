using System;
using System.Collections.Generic;

namespace DIY_API.Models;

public partial class Rating
{
    public int RatingId { get; set; }

    public int UserId { get; set; }

    public int ChallengeId { get; set; }

    public int? Stars { get; set; }

    public string? Message { get; set; }

    public DateTime? RatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreationDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public virtual Challenge Challenge { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
