using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services;

namespace LuongMinhPhuMVC.ViewComponents
{
    public class CategorySidebarViewComponent : ViewComponent
    {
        private readonly ICategoryService _categoryService;

        public CategorySidebarViewComponent(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            // Get current category ID from route if present
            var currentId = RouteData.Values["id"]?.ToString();
            short? categoryId = null;
            if (short.TryParse(currentId, out short parsedId))
            {
                categoryId = parsedId;
            }

            var categories = _categoryService.GetCategories().ToList();
            
            // We can also pass the current ID to the view to highlight it
            ViewBag.CurrentCategoryId = categoryId;

            return View(categories);
        }
    }
}
