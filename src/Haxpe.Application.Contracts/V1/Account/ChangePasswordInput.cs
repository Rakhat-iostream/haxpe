using System.ComponentModel.DataAnnotations;

namespace Haxpe.V1.Account
{
    public class ChangePasswordInput
    {
        public string CurrentPassword { get; set; }

        [Required]
        public string NewPassword { get; set; }
    }
}