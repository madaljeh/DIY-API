namespace DIY_API.Interfaces
{
    public interface IAdmin
    {
        Task<AdminStatisticsDTO> GetStatisticsAsync();
    }
}
