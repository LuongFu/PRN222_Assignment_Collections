using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace LuongMinhPhuMVC.ViewComponents
{
    public class NewsMenuViewComponent : ViewComponent
    {
        private readonly ICategoryService _categoryService;

        public NewsMenuViewComponent(ICategoryService categoryService)
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
