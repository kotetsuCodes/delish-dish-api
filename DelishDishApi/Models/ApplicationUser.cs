using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DelishDishApi.Data;
using Microsoft.AspNetCore.Identity;

namespace DelishDishApi.Models
{
  // Add profile data for application users by adding properties to the ApplicationUser class
  public class ApplicationUser : IdentityUser
  {
    public List<Recipe> Recipes { get; set; }
    public List<ShoppingList> ShoppingLists { get; set; }
  }
}