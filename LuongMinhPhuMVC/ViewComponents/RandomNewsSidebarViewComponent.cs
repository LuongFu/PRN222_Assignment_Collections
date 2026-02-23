using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LuongMinhPhuMVC.ViewComponents
{
    public class RandomNewsSidebarViewComponent : ViewComponent
    {
        private readonly FunewsManagementContext _context;

        public RandomNewsSidebarViewComponent(FunewsManagementContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var news = await _context.NewsArticles
                .OrderBy(r => Guid.NewGuid())
                .Take(5)
                .ToListAsync();

            return View(news);
        }
    }

}
