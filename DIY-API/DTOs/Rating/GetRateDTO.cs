﻿namespace DIY_API.DTOs.Rating
{
    public class GetRateDTO
    {
        public int UserId { get; set; }

        public int ChallengeId { get; set; }
        public string Username { get; set; } = null!;
        public string Title { get; set; } = null!;
        public int? Stars { get; set; }

        public string? Message { get; set; }

        public DateTime? RatedAt { get; set; }
    }
}
