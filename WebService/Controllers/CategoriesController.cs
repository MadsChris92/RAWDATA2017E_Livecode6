using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer;
using Microsoft.AspNetCore.Mvc;

namespace WebService.Controllers
{
    [Route("api/categories")]
    public class CategoriesController : Controller
    {
        [HttpGet]
        public IActionResult GetCategories()
        {
            var ds = new DataService();
            return Ok(ds.GetCategories());
        }
    }
}
