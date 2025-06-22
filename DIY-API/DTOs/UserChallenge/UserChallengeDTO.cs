namespace DIY_API.DTOs.UserChallenge
{
    public class UserChallengeDTO
    {
        public int UserId { get; set; }

        public int ChallengeId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime DueDate { get; set; }

        public string? Status { get; set; }
        public DateTime? CreationDate { get; set; }

        public string ChallengeTitle { get; set; }
        public string ChallengeDescription { get; set; }
        public int? EstimatedDuration { get; set; }
        public string? Level { get; set; }
    }
}
