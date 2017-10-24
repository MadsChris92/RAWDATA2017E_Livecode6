using System.Collections.Generic;

namespace DataAccessLayer
{
    public interface IDataService
    {
        IList<Category> GetCategories(int page, int pageSize);
        Category GetCategory(int id);

        int GetNumberOfCategories();
    }
}