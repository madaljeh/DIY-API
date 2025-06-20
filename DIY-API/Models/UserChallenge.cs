using System;
using System.Collections.Generic;

namespace DIY_API.Models;

public partial class UserChallenge
{
    public int UserChallengeId { get; set; }

    public int UserId { get; set; }

    public int ChallengeId { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime DueDate { get; set; }

    public string? Status { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreationDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public virtual Challenge Challenge { get; set; } = null!;

    public virtual ChallengeResult? ChallengeResult { get; set; }

    public virtual User User { get; set; } = null!;
}
