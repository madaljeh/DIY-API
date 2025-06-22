namespace DIY_API.DTOs.Challenge
{
    public class UpdateChallengesDTO
    {
        public string Title { get; set; } = null!;

        public string? Description { get; set; }

        public string? MediaUrl { get; set; }

        public string? Level { get; set; }

        public int? EstimatedDuration { get; set; }

        public int CategoryId { get; set; }

        public string? UpdatedBy { get; set; }

        public bool? IsActive { get; set; }

        public DateTime? UpdateDate { get; set; }
    }
}
