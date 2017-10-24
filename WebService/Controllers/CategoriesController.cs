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
        private readonly IDataService _dataService;

        public CategoriesController(IDataService dataService)
        {
            _dataService = dataService;
        }

        [HttpGet]
        public IActionResult GetCategories(int page = 0, int pageSize = 2)
        {
            if (pageSize > 50) pageSize = 50;
            //pageSize = pageSize > 50 ? 50 : pageSize;

           
            return Ok(_dataService.GetCategories(page, pageSize));
        }

        [HttpGet("{id}")]
        public IActionResult GetCategory(int id)
        {
            var category = _dataService.GetCategory(id);
            if (category == null) return NotFound();
            return Ok(category);
        }
    }
}
