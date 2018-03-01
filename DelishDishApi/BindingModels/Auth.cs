using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DelishDishApi.BindingModels
{
  public class AuthRequest
  {
    //[Required]
    public string Email { get; set; }

    //[Required]
    public string Password { get; set; }
  }

  public class RegisterRequest
  {
    //[Required]
    public string Email { get; set; }

    //[Required]
    //[StringLength(100, ErrorMessage = "PASSWORD_MIN_LENGTH", MinimumLength = 8)]
    public string Password { get; set; }
  }
}