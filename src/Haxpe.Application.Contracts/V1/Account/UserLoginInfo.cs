using System.ComponentModel.DataAnnotations;

namespace Haxpe.V1.Account
{
    public class UserLoginInfo
    {
        [Required]
        [StringLength(255)]
        public string Email { get; set; }

        [Required]
        [StringLength(32)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}