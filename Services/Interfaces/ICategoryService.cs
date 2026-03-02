using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface ICategoryService
    {
        IEnumerable<Category> GetCategories();
        Category GetCategoryById(short categoryId);
        Category AddCategory(Category category);
        Category UpdateCategory(Category category);
        Category DeleteCategory(short categoryId);
        bool CategoryExists(short categoryId);
    }
}
