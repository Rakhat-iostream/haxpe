using System.ComponentModel.DataAnnotations;

namespace Haxpe.V1.Account
{
    public class RegisterDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string FullName { get; set; }

        [Required]
        public string Phone { get; set; }

        [MaxLength(6)]
        public string? PreferLanguage { get; set; }

    }
}