﻿namespace backend.Models.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
