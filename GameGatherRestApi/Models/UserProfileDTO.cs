﻿namespace GameGatherRestApi.Models
{
    public class UserProfileDTO
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public string? City { get; set; }
        public string? Address { get; set; }
    }
}
