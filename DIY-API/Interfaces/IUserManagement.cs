using DIY_API.DTOs.User;

namespace DIY_API.Interfaces
{
    public interface IUserManagement
    {
        Task<List<GetAllUsersDTO>> GetAllUsers();
        Task<GetAllUsersDTO> GetUserById(int userId);
        Task<string> CreateAdmin(CreateAdminDTO input);
        Task<bool> DeleteUser(int userId);
        Task<string> UpdateUserInfo(int userId, UpdateUserInfoDTO input);

    }
}
