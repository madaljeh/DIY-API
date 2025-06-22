namespace DIY_API.DTOs.Challenge
{
    public class TopRatedChallengesDTO
    {
        public int ChallengesId { get; set; }
        public string Title { get; set; } = null!;

        public string? Description { get; set; }

        public string? MediaUrl { get; set; }

        public string? Level { get; set; }

        public int? EstimatedDuration { get; set; }

        public int CategoryId { get; set; }

        public double? ChallengeRate { get; set; }

    }
}
