using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DelishDishApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DelishDishApi.Controllers
{
  [Route("[controller]/[action]")]
  [Authorize]
  public class ShoppingListController : Controller
  {
    private readonly UserManager<ApplicationUser> _userManager;

    public ShoppingListController(
      UserManager<ApplicationUser> userManager)
    {
      _userManager = userManager;
    }

    [HttpGet]
    public async Task<object> GetShoppingLists()
    {
      var result = await _userManager.GetUserAsync(HttpContext.User);

      return result.ShoppingLists ?? new List<Data.ShoppingList>();
    }
  }
}