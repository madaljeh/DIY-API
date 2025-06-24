using DIY_API.DTOs.Authantication;
using DIY_API.Helpers;
using DIY_API.Interfaces;
using DIY_API.Models;
using EmailServicePackage;
using EmailServicePackage.Interfaces;
using Microsoft.EntityFrameworkCore;
using SendGrid.Helpers.Mail;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DIY_API.Services
{
    public class AuthanticationService : IAuthantication
    {
        private readonly DIYDbContext _diycontext;
        private readonly ISmtpService _smtpService;
        public AuthanticationService(DIYDbContext context, ISmtpService smtpService)
        {
            _diycontext = context;
            _smtpService = smtpService;
        }

      

    

        public async Task<string> SignIn(SignInInputDTO input)
        {
            input.Password = HashingHelper.HashValueWith384(input.Password);
            var user = _diycontext.Users
                .Where(u => (u.Email == input.Username || u.Username == input.Username) && u.Password == input.Password)
                .SingleOrDefault();

            if (user == null)
            {
                return "Invalid username or password.";
            }
            if (!user.IsVerified)
            {
                return "Your account is not verified.";
            }
            if (!user.IsActive)
            {
                return "Your account is inactive.";
            }
            if (user.IsLogedIn)
            {
                return "User is already logged in.";
            }

            user.IsLogedIn = true;
            _diycontext.Update(user);
            await _diycontext.SaveChangesAsync();

            var token = TokenHelper.GenerateJWTToken(user.UserId.ToString(), "Customer");

            return $"User signed in successfully. Token: {token}";

        }

        public async Task<bool> SignOut(int userId)
        {
            var u = _diycontext.Users.Where(u => u.UserId == userId && u.IsLogedIn == true).SingleOrDefault();
            if (u == null)
            {
                return false;
            }
            u.IsLogedIn = false;
            _diycontext.Users.Update(u);
            await _diycontext.SaveChangesAsync();

            return true;
        }

        public async Task<string> SignUp(SignUpInputDTO input)
        {

            var user = new User();
            user.FirstName = input.FirstName;
            user.LastName = input.LastName;
            user.Username = input.Username;
            user.Password = HashingHelper.HashValueWith384(input.Password);
            user.Email = input.Email;
            user.PhoneNumber = input.PhoneNumber;
            user.Age= input.Age;
            user.Gender = input.Gender;

            user.ProfileImage = input.ProfileImage ?? "default.png";
            user.RoleId = 2;
            user.IsActive = true;
            user.IsVerified = true; 


            try
            {
                _diycontext.Users.Add(user);
                await _diycontext.SaveChangesAsync();
                return "User registered successfully.";
            }
            catch (DbUpdateException dbEx)
            {
                return $"Database error: {dbEx.InnerException?.Message ?? dbEx.Message}";
            }
            catch (Exception ex)
            {
                return $"An error occurred: {ex.Message}";
            }


        }

        
    }
}
