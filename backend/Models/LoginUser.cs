﻿namespace backend.Models
{
    public class LoginUser
    {
        public required string Email { get; set; }
        public required string Password
        {
            get; set;
        }
    }
}
