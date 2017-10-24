using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DataAccessLayer;
using Microsoft.AspNetCore.Mvc;
using WebService.Models;

namespace WebService.Controllers
{
    [Route("api/categories")]
    public class CategoriesController : Controller
    {
        private readonly IDataService _dataService;
        private readonly IMapper _mapper;

        public CategoriesController(IDataService dataService, IMapper mapper)
        {
            _dataService = dataService;
            _mapper = mapper;
        }

        [HttpGet(Name = nameof(GetCategories))]
        public IActionResult GetCategories(int page = 0, int pageSize = 2)
        {
            CheckPageSize(ref pageSize);
            
            var total = _dataService.GetNumberOfCategories();
            var totalPages = GetTotalPages(pageSize, total);

            var data = _dataService.GetCategories(page, pageSize)
                .Select(x => new SimpleCategoryModel
                {
                    Url = Url.Link(nameof(GetCategory), new { id = x.Id }),
                    Name = x.Name
                });

            var route = nameof(GetCategories);

            var result = new
            {
                Total = total,
                Pages = totalPages,
                Page = page,
                Prev = Link(route, page, pageSize, -1, () => page > 0),
                Next = Link(route, page, pageSize, 1, () => page < totalPages - 1),
                Url = Link(route, page, pageSize),
                Data = data
            };
                       
            return Ok(result);
        }

        [HttpGet("{id}", Name = nameof(GetCategory))]
        public IActionResult GetCategory(int id)
        {
            var category = _dataService.GetCategory(id);
            if (category == null) return NotFound();

            var model = _mapper.Map<CategoryModel>(category);
            model.Url = Url.Link(nameof(GetCategory), new {id = category.Id});

            return Ok(model);
        }

        // Helpers 

        private string Link(string route, int page, int pageSize, int pageInc = 0, Func<bool> f = null)
        {
            if (f == null) return Url.Link(route, new { page, pageSize });

            return f()
                ? Url.Link(route, new { page = page + pageInc, pageSize })
                : null;
        }

        private static int GetTotalPages(int pageSize, int total)
        {
            return (int)Math.Ceiling(total / (double)pageSize);
        }

        private static void CheckPageSize(ref int pageSize)
        {
            pageSize = pageSize > 50 ? 50 : pageSize;
        }
    }
}
