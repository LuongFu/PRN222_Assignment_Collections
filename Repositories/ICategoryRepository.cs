using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetCategories();
        Category AddCategory(Category category);
        Category UpdateCategory(Category category);
        Category DeleteCategory(short categoryId);
    }
}
