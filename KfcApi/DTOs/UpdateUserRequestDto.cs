using System.ComponentModel.DataAnnotations;

namespace KfcApi.DTOs
{
    public class UpdateUserRequestDto
    {
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        [DataType(DataType.Password)]
        public string? Password { get; set; }  // Current password
        [DataType(DataType.Password)]
        public string? NewPassword { get; set; }  // New password
        public IFormFile? Image { get; set; }

    }
}
