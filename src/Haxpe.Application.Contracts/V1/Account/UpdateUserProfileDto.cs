using System;
using System.ComponentModel.DataAnnotations;

namespace Haxpe.V1.Account
{
  public class UpdateUserProfileDto
  {
    public string? Name { get; set; }

    public string? Surname { get; set; }

    public string? PhoneNumber { get; set; }

    [MaxLength(6)]
    public string? PreferLanguage { get; set; }
  }
}
