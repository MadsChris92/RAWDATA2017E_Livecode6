using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccessLayer
{
    public class DataService
    {
        public IList<Category> GetCategories(int page, int pageSize)
        {
            using (var db = new NorthwindContext())
            {
                return db.Categories
                    .OrderBy(x => x.Id)
                    .Skip(page*pageSize)
                    .Take(pageSize)
                    .ToList();
            }
        }

        public Category GetCategory(int id)
        {
            using (var db = new NorthwindContext())
            {
                return db.Categories.Find(id);
            }
        }
    }
}
