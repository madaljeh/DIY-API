namespace DIY_API.DTOs.ChallengeResultDTO
{
    public class GetChallengeResultDTO
    {
        public int ChallengeResultId { get; set; }

        public int UserChallengeId { get; set; }
        public string Title { get; set; } = null!;

        public string FileUrl { get; set; } = null!;

        public DateTime? UploadedAt { get; set; }
    }
}
