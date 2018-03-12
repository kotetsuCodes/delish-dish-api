using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using DelishDishApi.Models;
using DelishDishApi.Models.AccountViewModels;
using DelishDishApi.Services;
using Microsoft.Extensions.Configuration;
using DelishDishApi.BindingModels;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace DelishDishApi.Controllers
{
  [Route("[controller]/[action]")]
  public class AccountController : Controller
  {
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IConfiguration _configuration;

    public AccountController(
      UserManager<ApplicationUser> userManager,
      SignInManager<ApplicationUser> signInManager,
      IConfiguration configuration)
    {
      _userManager = userManager;
      _signInManager = signInManager;
      _configuration = configuration;
    }

    [HttpPost]
    public async Task<object> Login([FromBody] AuthRequest authRequest)
    {
      var result = await _signInManager.PasswordSignInAsync(authRequest.Email, authRequest.Password, false, false);

      if(result.Succeeded)
      {
        var appUser = _userManager.Users.SingleOrDefault(u => u.Email == authRequest.Email);
        return new { Token = await generateJwtToken(authRequest.Email, appUser) };
      }

      throw new ApplicationException("INVALID_LOGIN_ATTEMPT");
    }

    [HttpPost]
    public async Task<object> Register([FromBody] RegisterRequest registerRequest)
    {
      var user = new ApplicationUser
      {
        UserName = registerRequest.Email,
        Email = registerRequest.Email
      };

      var result = await _userManager.CreateAsync(user, registerRequest.Password);

      if(result.Succeeded)
      {
        await _signInManager.SignInAsync(user, false);
        return await generateJwtToken(registerRequest.Email, user);
      }

      throw new ApplicationException("UNKNOWN_ERROR");
    }

    private async Task<object> generateJwtToken(string email, IdentityUser user)
    {
      var claims = new List<Claim>
      {
        new Claim(JwtRegisteredClaimNames.Sub, email),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        new Claim(ClaimTypes.NameIdentifier, user.Id)
      };

      var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtKey"]));
      var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
      var expires = DateTime.Now.AddDays(Convert.ToDouble(_configuration["JwtExpireDays"]));

      var token = new JwtSecurityToken(
        _configuration["JwtIssuer"],
        _configuration["JwtIssuer"],
        claims,
        expires: expires,
        signingCredentials: credentials
        );

      return new JwtSecurityTokenHandler().WriteToken(token);
    }
  }
}