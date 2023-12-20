﻿namespace KfcApi.DTOs
{
    public class LoginResponseDto
    {
        public string JwtToken { get; set; }
        public string UserId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }

    }
}
