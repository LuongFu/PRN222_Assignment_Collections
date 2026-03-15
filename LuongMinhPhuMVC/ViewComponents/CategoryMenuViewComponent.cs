using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace LuongMinhPhuMVC.ViewComponents
{
    public class CategoryMenuViewComponent : ViewComponent
    {
        private readonly ICategoryService _categoryService;

        public CategoryMenuViewComponent(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = _categoryService.GetCategories()
                .Where(c => c.IsActive != false)
                .ToList();
            return View(categories);
        }
    }
}
