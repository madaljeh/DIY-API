﻿namespace DIY_API.DTOs.Authantication
{
    public class SignUpInputDTO
    {
        public string FirstName { get; set; } 

        public string LastName { get; set; } 

        public string Username { get; set; } 

        public string Password { get; set; } 
        public string Email { get; set; }

        public int Age { get; set; }

        public string Gender { get; set; } 

        public string? PhoneNumber { get; set; }

        public string? ProfileImage { get; set; }


    }
}
