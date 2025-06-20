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

        public async Task<bool> ResetPersonPassword(ResetPersonPasswordInputDTO input)
        {
            var user = _diycontext.Users.Where(u => u.Email == input.Email && u.Otp == input.Otp
                     && u.IsLogedIn == false && u.ExpireOtp > DateTime.Now).SingleOrDefault();
            if (user == null)
            {
                return false;
            }
            if (input.Password != input.ConfirmPassword)
            {
                return false;
            }
            user.Password = HashingHelper.HashValueWith384(input.ConfirmPassword);
            user.Otp = null;
            user.ExpireOtp = null;
            
            _diycontext.Update(user);
            await _diycontext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> SendOTP(string email)
        {
            var user = _diycontext.Users.Where(x => x.Email == email && x.IsLogedIn == false).SingleOrDefault();
            if (user == null)
            {
                return false;
            }
            Random random = new Random();
            var otp = random.Next(11111, 99999);
            user.Otp = otp.ToString();
            user.ExpireOtp = DateTime.Now.AddMinutes(5);
            await _smtpService.SendEmailAsync(new SendEmailDto()
            {
                To = email,
                Subject = "Your OTP",
                Body = $"{user.Otp} valid until {user.ExpireOtp}"
            });
            _diycontext.Users.Update(user);
            await _diycontext.SaveChangesAsync();
            return true;
        }

        public async Task<string> SignIn(SignInInputDTO input)
        {
            input.Password = HashingHelper.HashValueWith384(input.Password);
            var user = _diycontext.Users
                 .Where(u => (u.Email == input.Username || u.Username == input.Username) && u.Password == input.Password && u.IsLogedIn == false)
                .SingleOrDefault();

            if (user == null)
            {
                return "Invalid username or password.";
            }
            user.IsLogedIn = true;
            _diycontext.Update(user);
           await  _diycontext.SaveChangesAsync();
            
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
            user.ProfileImage = input.ProfileImage ?? "default.png";
            user.RoleId = 2;
            user.IsActive = true;



            Random random = new Random();
            var otp = random.Next(11111, 99999);
            user.Otp = otp.ToString();
            user.ExpireOtp = DateTime.Now.AddMinutes(5);

            try
            {
                _diycontext.Users.Add(user);
                await _diycontext.SaveChangesAsync(); 

               
                await _smtpService.SendEmailAsync(new SendEmailDto
                {
                    To = input.Email,
                    Subject = "Your OTP",
                    Body = $"{user.Otp} (valid until {user.ExpireOtp:HH:mm})"
                });

                return "Verification email sent. Please check your inbox.";
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

        public async Task<string> Verification(VerificationInputDTO input)
        {
            var user = _diycontext.Users.Where(u => (u.Email == input.Email || u.Username == input.Email) && u.Otp == input.Otp
             && u.IsLogedIn == false && u.ExpireOtp > DateTime.Now).SingleOrDefault();
            if (user == null)
            {
                return "User not found";
            }

            if (input.IsLogedIn)
            {
                user.IsVerified = true;
                user.ExpireOtp = null;
                user.Otp = null;
                _diycontext.Update(user);
                await _diycontext.SaveChangesAsync();
                return "Your Account Is Verifyed";
            }
            else
            {
                
                user.LastLoginTime = DateTime.Now;
                user.IsLogedIn = false;
                user.ExpireOtp = null;
                user.Otp = null;

                _diycontext.Update(user);
                await _diycontext.SaveChangesAsync();

                var response = TokenHelper.GenerateJWTToken(user.UserId.ToString(), "Customer");
                return response;
            }
        }
    }
}
