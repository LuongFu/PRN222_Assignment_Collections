using BusinessObjects;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public IEnumerable<Category> GetCategories()
            => _categoryRepository.GetCategories();
        public Category AddCategory(Category category)
            => _categoryRepository.AddCategory(category);
        public Category UpdateCategory(Category category)
            => _categoryRepository.UpdateCategory(category);
        public Category DeleteCategory(short categoryId)
            => _categoryRepository.DeleteCategory(categoryId);

        public Category GetCategoryById(short categoryId)
            => _categoryRepository.GetCategoryById(categoryId);

        public bool CategoryExists(short categoryId)
            => _categoryRepository.CategoryExists(categoryId);
    }
}
