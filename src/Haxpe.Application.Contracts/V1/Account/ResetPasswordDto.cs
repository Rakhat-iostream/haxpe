using System;
using System.ComponentModel.DataAnnotations;

namespace Haxpe.V1.Account
{
    public class ResetPasswordDto
    {
        public string Email { get; set; }

        [Required]
        public string ResetToken { get; set; }

        [Required]
        public string Password { get; set; }
    }
}