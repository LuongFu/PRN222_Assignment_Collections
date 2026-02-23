using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LuongMinhPhuMVC.Controllers
{
    public class ProfileController : Controller
    {
        private readonly FunewsManagementContext _context;

        public ProfileController(FunewsManagementContext context)
        {
            _context = context;
        }

        // GET: Profile
        public async Task<IActionResult> Index()
        {
            var userId = HttpContext.Session.GetInt32("USERID");
            if (userId == null)
                return RedirectToAction("Login", "Accounts");

            var user = await _context.SystemAccounts
                .FirstOrDefaultAsync(u => u.AccountId == userId);

            if (user == null)
                return NotFound();

            return View(user);
        }

        // POST: Profile Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(SystemAccount model, string? NewPassword)
        {
            var userId = HttpContext.Session.GetInt32("USERID");
            if (userId == null)
                return RedirectToAction("Login", "Accounts");

            var user = await _context.SystemAccounts
                .FirstOrDefaultAsync(u => u.AccountId == userId);

            if (user == null)
                return NotFound();

            if (ModelState.IsValid)
            {
                user.AccountName = model.AccountName;
                user.AccountEmail = model.AccountEmail;

                if (!string.IsNullOrEmpty(NewPassword))
                {
                    user.AccountPassword = NewPassword;
                    // Nếu muốn chuyên nghiệp hơn thì hash password
                }

                await _context.SaveChangesAsync();

                ViewBag.Message = "Profile updated successfully!";
            }

            return View(user);
        }
    }
}
