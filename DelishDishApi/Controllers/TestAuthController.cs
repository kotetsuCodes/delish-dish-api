using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DelishDishApi.Controllers
{
  [Route("[controller]/[action]")]
  [Authorize]
  public class TestAuthController : Controller
  {
    [HttpGet]
    public async Task<object> Test()
    {
      return await Task.FromResult<object>("Auth is working");
    }
  }
}