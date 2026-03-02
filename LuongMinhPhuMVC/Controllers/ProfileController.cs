using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services;

namespace LuongMinhPhuMVC.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IAccountService _accountService;

        public ProfileController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        // GET: Profile
        public IActionResult Index()
        {
            var userId = HttpContext.Session.GetInt32("USERID");
            if (userId == null)
                return RedirectToAction("Login", "Account");

            var user = _accountService.GetAccountById((short)userId.Value);

            if (user == null)
                return NotFound();

            return View(user);
        }

        // POST: Profile Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(SystemAccount model, string? NewPassword)
        {
            var userId = HttpContext.Session.GetInt32("USERID");
            if (userId == null)
                return RedirectToAction("Login", "Account");

            var user = _accountService.GetAccountById((short)userId.Value);

            if (user == null)
                return NotFound();

            if (ModelState.IsValid)
            {
                user.AccountName = model.AccountName;
                user.AccountEmail = model.AccountEmail;

                if (!string.IsNullOrEmpty(NewPassword))
                {
                    user.AccountPassword = NewPassword;
                }

                _accountService.Update(user);

                ViewBag.Message = "Profile updated successfully!";
            }

            return View(user);
        }
    }
}
