using BusinessObjects;
using DataAccessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly CategoryDAO _categoryDAO;
        public CategoryRepository(CategoryDAO categoryDAO)
        {
            _categoryDAO = categoryDAO;
        }
        public Category AddCategory(Category category)
        {
            return _categoryDAO.AddCategory(category);
        }

        public Category DeleteCategory(short categoryId)
        {
            return _categoryDAO.DeleteCategory(categoryId);
        }

        public IEnumerable<Category> GetCategories()
        {
            return _categoryDAO.GetCategories();
        }

        public Category UpdateCategory(Category category)
        {
            return _categoryDAO.UpdateCategory(category);
        }

        public Category GetCategoryById(short categoryId)
        {
            return _categoryDAO.GetCategoryById(categoryId);
        }

        public bool CategoryExists(short categoryId)
        {
            return _categoryDAO.CategoryExists(categoryId);
        }
    }
}
