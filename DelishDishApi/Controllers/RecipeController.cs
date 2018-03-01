using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DelishDishApi.Data;
using DelishDishApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DelishDishApi.Controllers
{
  [Route("[controller]/[action]")]
  [Authorize]
  public class RecipeController : Controller
  {
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ApplicationDbContext _dbContext;

    public RecipeController(
      UserManager<ApplicationUser> userManager, ApplicationDbContext context)
    {
      _dbContext = context;
      _userManager = userManager;
    }

    public async Task<object> GetRecipes()
    {
      throw new NotImplementedException();
    }

    public async Task<IActionResult> CreateRecipe([FromBody]Data.Recipe model)
    {
      var result = await _userManager.GetUserAsync(HttpContext.User);
      var user = await _dbContext.Users.Include("Recipes.IngredientDetails").SingleOrDefaultAsync(u => u.Id == result.Id);

      //model.IngredientDetails.AddRange(model.IngredientDetails);
      result.Recipes.Add(model);

      await _userManager.UpdateAsync(result);

      return NoContent();
    }
  }
}