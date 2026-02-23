using BusinessObjects;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class CatergoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CatergoryService(ICategoryRepository categoryRepository)
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
    }
}
