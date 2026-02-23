using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LuongMinhPhuMVC.ViewComponents
{
    public class TagSidebarViewComponent : ViewComponent
    {
        private readonly FunewsManagementContext _context;

        public TagSidebarViewComponent(FunewsManagementContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var tags = await _context.Tags.ToListAsync();
            return View(tags);
        }
    }

}
