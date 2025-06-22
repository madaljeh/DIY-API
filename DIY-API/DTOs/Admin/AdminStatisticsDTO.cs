using DIY_API.DTOs.Challenge;

public class AdminStatisticsDTO
{
    public int TotalUsers { get; set; }
    public int TotalChallenges { get; set; }
    public int TotalUserChallenges { get; set; }
    public int CompletedChallenges { get; set; }
    public int PendingChallenges { get; set; }
    public int ExpiredChallenges { get; set; }

    public List<TopRatedChallengesDTO> TopRatedChallenges { get; set; } = new();
    public List<MostActiveUserDTO> MostActiveUsers { get; set; } = new();

    public class MostActiveUserDTO
    {
        public string Username { get; set; }
        public int Completed { get; set; }
    }
}
