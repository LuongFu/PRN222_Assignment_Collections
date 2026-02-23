using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LuongMinhPhuMVC.ViewComponents
{
    public class CategorySidebarViewComponent : ViewComponent
    {
        private readonly FunewsManagementContext _context;

        public CategorySidebarViewComponent(FunewsManagementContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await _context.Categories.ToListAsync();
            return View(categories);
        }
    }
}
