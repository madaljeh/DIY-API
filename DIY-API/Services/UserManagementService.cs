using DIY_API.DTOs.User;
using DIY_API.Helpers;
using DIY_API.Interfaces;
using DIY_API.Models;
using EmailServicePackage.Interfaces;
using Microsoft.EntityFrameworkCore;
using SendGrid.Helpers.Mail;

namespace DIY_API.Services
{
    public class UserManagementService : IUserManagement
    {
        private readonly DIYDbContext _diycontext;
       
        public UserManagementService(DIYDbContext context)
        {
            _diycontext = context;
            
        }

        public async Task<string> CreateAdmin(CreateAdminDTO input)
        {

            var user = new User
            {
                FirstName = input.FirstName,
                LastName = input.LastName,
                Username = input.Username,
                Password = HashingHelper.HashValueWith384(input.Password),
                Email = input.Email,
                PhoneNumber = input.PhoneNumber,
                Age = input.Age,
                Gender = input.Gender,
                ProfileImage = input.ProfileImage,
                IsActive = true, 
                RoleId = 1, 
                CreationDate = DateTime.UtcNow
            };
            _diycontext.Users.Add(user);
            await _diycontext.SaveChangesAsync();
            return "Admin created successfully";
        }

        public async Task<bool> DeleteUser(int userId)
        {
           var user = await _diycontext.Users.FindAsync(userId);
            if (user == null)
            {
                return false; 
            }
            _diycontext.Users.Remove(user);
            await _diycontext.SaveChangesAsync();
            return true; 
        }

        public async Task<List<GetAllUsersDTO>> GetAllUsers()
        {
         
            var users = _diycontext.Users.ToList();
            var userList = users.Select(user => new GetAllUsersDTO
            {
                UserId = user.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.Username,
                Password = user.Password,
                Email = user.Email,
                Age= user.Age,
                Gender = user.Gender,
                PhoneNumber = user.PhoneNumber,
                ProfileImage = user.ProfileImage,
                IsActive = user.IsActive,
                RoleId = user.RoleId,
                CreationDate = user.CreationDate
            }).ToList();

            return  userList;

        }

        public async Task<GetAllUsersDTO> GetUserById(int userId)
        {

            var user = _diycontext.Users.Where(u => u.UserId == userId).SingleOrDefault();
           
            var userDto = new GetAllUsersDTO
            {
                UserId = user.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.Username,
                Password = user.Password,
                Email = user.Email,
                Age= user.Age,
                Gender = user.Gender,
                PhoneNumber = user.PhoneNumber,
                ProfileImage = user.ProfileImage,
                IsActive = user.IsActive,
                RoleId = user.RoleId,
                CreationDate = user.CreationDate
            };
            return userDto;
        }

        public async Task<string> UpdateUserInfo(int userId, UpdateUserInfoDTO input)
        {
           var user = await _diycontext.Users.FindAsync(userId);
            if (user == null)
            {
                return "User not found";
            }
            user.FirstName = input.FirstName;
            user.LastName = input.LastName;
            user.Username = input.Username;
            user.Email = input.Email;
            user.Age = input.Age;
            user.Gender = input.Gender;
            user.PhoneNumber = input.PhoneNumber;
            user.ProfileImage = input.ProfileImage;
         
            _diycontext.Users.Update(user);
            await _diycontext.SaveChangesAsync();

            return "User information updated successfully";
        }

        public async Task<bool> UserActivationStatus(int userId, UserActivationDTO input)
        {
         var user = await _diycontext.Users.FindAsync(userId);
            if (user == null)
            {
                return false; 
            }
            user.IsActive = input.IsActive;
            _diycontext.Users.Update(user);
            await _diycontext.SaveChangesAsync();
            return true;
        }
    }
}
