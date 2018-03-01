using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using DelishDishApi.Models;

namespace DelishDishApi.Data
{
  public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
  {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
      base.OnModelCreating(builder);
      // Customize the ASP.NET Identity model and override the defaults if needed. For example, you
      // can rename the ASP.NET Identity table names and more. Add your customizations after calling base.OnModelCreating(builder);
    }

    public DbSet<Recipe> Recipes { get; set; }
    public DbSet<ShoppingList> ShoppingLists { get; set; }
    public DbSet<IngredientDetail> IngredientDetails { get; set; }
    public DbSet<CommonIngredient> CommonIngredients { get; set; }
  }

  public class ShoppingList
  {
    public int ShoppingListId { get; set; }
    public string Name { get; set; }

    public List<Recipe> Recipes { get; set; }
  }

  public class Recipe
  {
    public int RecipeId { get; set; }
    public string Name { get; set; }

    public List<IngredientDetail> IngredientDetails { get; set; }
  }

  public class IngredientDetail
  {
    public int IngredientDetailId { get; set; }
    public string Name { get; set; }
    public string AmountLabel { get; set; }
    public string AmountValue { get; set; }
  }

  public class CommonIngredient
  {
    public int CommonIngredientId { get; set; }
    public string Name { get; set; }
  }
}