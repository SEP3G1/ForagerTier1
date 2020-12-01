﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ForagerTier1.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Required, MaxLength(32)]
        public string Name { get; set; }
        [Required]
        public int SecurityLevel { get; set; }
        [Required, MaxLength(64)]
        public string Password { get; set; }
        [Required, MaxLength(64)]
        public string Email { get; set; }
        public int CompanyId { get; set; }

        public string GetUserLevel()
        {
            string l = "";
            switch (SecurityLevel)
            {
                case 1: l = "Employee"; break;
                case 2: l = "Company Administrator"; break;
                case 3: l = "Moderator"; break;
                case 4: l = "Admin"; break;
            }
            return l;
        }
    }
}
