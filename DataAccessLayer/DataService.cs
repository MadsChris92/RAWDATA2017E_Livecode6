using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccessLayer
{
    public class DataService
    {
        public IList<Category> GetCategories()
        {
            using (var db = new NorthwindContext())
            {
                return db.Categories.ToList();
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
