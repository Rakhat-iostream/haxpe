using System.ComponentModel.DataAnnotations;

namespace Haxpe.V1.Account
{
    public class SendPasswordResetCodeDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string ReturnUrl { get; set; }

        public string ReturnUrlHash { get; set; }
    }
}