namespace DIY_API.DTOs.Rating
{
    public class CreateRateDTO
    {
        public int UserId { get; set; }

        public int ChallengeId { get; set; }

        public int? Stars { get; set; }

        public string? Message { get; set; }
    }
}
