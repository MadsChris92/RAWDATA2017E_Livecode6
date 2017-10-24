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
        public IActionResult GetCategories(int page = 0, int pageSize = 2)
        {
            if (pageSize > 50) pageSize = 50;
            //pageSize = pageSize > 50 ? 50 : pageSize;

            var ds = new DataService();
            return Ok(ds.GetCategories(page, pageSize));
        }
    }
}
