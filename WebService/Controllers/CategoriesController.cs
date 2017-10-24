using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer;
using Microsoft.AspNetCore.Mvc;
using WebService.Models;

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

        [HttpGet(Name = nameof(GetCategories))]
        public IActionResult GetCategories(int page = 0, int pageSize = 2)
        {
            if (pageSize > 50) pageSize = 50;
            //pageSize = pageSize > 50 ? 50 : pageSize;

            var total = _dataService.GetNumberOfCategories();
            var totalPages = (int)Math.Ceiling(total / (double)pageSize);

            var data = _dataService.GetCategories(page, pageSize)
                .Select(x => new SimpleCategoryModel
                {
                    Url = Url.Link(nameof(GetCategory), new { id = x.Id }),
                    Name = x.Name
                });

            

            var prev = page > 0 
                ? Url.Link(nameof(GetCategories), new { page = page -1, pageSize }) 
                : null;

            var next = page < totalPages - 1 
                ? Url.Link(nameof(GetCategories), new { page = page + 1, pageSize }) 
                : null;

            var result = new
            {
                Total = total,
                Prev = prev,
                Next = next,
                Data = data
            };
                       
            return Ok(result);
        }

        [HttpGet("{id}", Name = nameof(GetCategory))]
        public IActionResult GetCategory(int id)
        {
            var category = _dataService.GetCategory(id);
            if (category == null) return NotFound();

            var model = new CategoryModel
            {
                Url = Url.Link(nameof(GetCategory), new { id = category.Id }),
                Name = category.Name,
                Description = category.Description
            };

            return Ok(model);
        }
    }
}
