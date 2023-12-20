﻿using System.ComponentModel.DataAnnotations;

namespace KfcApi.DTOs
{
    public class RegisterRequestDto
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string[]? Roles { get; set; } 
    }
}
