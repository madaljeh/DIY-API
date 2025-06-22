namespace DIY_API.DTOs.ChallengeResultDTO
{
    public class GetChallengeResultByIDDTO
    {
        public string Title { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string FileUrl { get; set; } = null!;
        public DateTime? UploadedAt { get; set; }
    }
}
