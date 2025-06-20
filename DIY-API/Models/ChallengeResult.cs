using System;
using System.Collections.Generic;

namespace DIY_API.Models;

public partial class ChallengeResult
{
    public int ChallengeResultId { get; set; }

    public int UserChallengeId { get; set; }

    public string FileUrl { get; set; } = null!;

    public DateTime? UploadedAt { get; set; }

    public DateTime? CreationDate { get; set; }

    public virtual UserChallenge UserChallenge { get; set; } = null!;
}
