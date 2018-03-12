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

namespace DelishDishApi.Controllers
{
  [Route("[controller]/[action]")]
  [Authorize]
  public class IngredientController : Controller
  {
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ApplicationDbContext _dbContext;

    public IngredientController(
     UserManager<ApplicationUser> userManager, ApplicationDbContext context)
    {
      _dbContext = context;
      _userManager = userManager;
    }

    [HttpPost]
    public async Task<object> GetIngredients()
    {
      return _dbContext.CommonIngredients.ToList() ?? new List<Data.CommonIngredient>();
    }

    [HttpPost]
    public async Task<object> CreateIngredient([FromBody] Data.CommonIngredient ingredient)
    {
      _dbContext.Add(ingredient);
      await _dbContext.SaveChangesAsync();

      return ingredient;
    }
  }
}