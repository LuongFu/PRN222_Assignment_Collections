using BusinessObjects;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositories;
using Services;
using System.Security.Claims;

namespace LuongMinhPhuMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IConfiguration _config;
        private readonly IAccountService _accountService;

        public AccountController(IConfiguration config, IAccountService accountService)
        {
            _config = config;
            _accountService = accountService;
        }

        public IActionResult Login() => View();

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            var adminEmail = _config["AdminAccount:Email"];
            var adminPass = _config["AdminAccount:Password"];

            if (email == adminEmail && password == adminPass)
            {
                HttpContext.Session.SetInt32("ROLE", 0);
                HttpContext.Session.SetInt32("USERID", -1);

                return RedirectToAction("Index", "Admin");
            }

            var account = _accountService.Login(email, password);
            if (account == null) return View();

            HttpContext.Session.SetInt32("ROLE", (int)account.AccountRole);
            HttpContext.Session.SetInt32("USERID", account.AccountId);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult GoogleLogin()
        {
            var props = new AuthenticationProperties
            {
                RedirectUri = Url.Action("GoogleResponse")
            };

            return Challenge(props, GoogleDefaults.AuthenticationScheme);
        }
        public async Task<IActionResult> GoogleResponse()
        {
            var result = await HttpContext.AuthenticateAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);

            if (!result.Succeeded)
                return RedirectToAction("Login");

            var email = result.Principal
                .FindFirst(ClaimTypes.Email)?.Value;

            if (email == null)
                return RedirectToAction("Login");

            var loginResult = _accountService.GoogleLogin(email);

            if (!loginResult.IsSuccess)
            {
                ViewBag.Error = loginResult.Error;
                return View("Login");
            }

            HttpContext.Session.SetInt32("ROLE", loginResult.Role!.Value);
            HttpContext.Session.SetInt32("USERID", loginResult.UserId!.Value);

            return RedirectToAction("Index", "Home");
        }
        public IActionResult Index(string search)
        {
            if (!IsAdmin()) return RedirectToAction("Login", "Account");

            var accounts = _accountService.GetAll();

            if (!string.IsNullOrEmpty(search))
            {
                accounts = accounts
                    .Where(a => a.AccountEmail.Contains(search))
                    .ToList();
            }

            return View(accounts);
        }

        public IActionResult Create()
        {
            if (!IsAdmin()) return RedirectToAction("Login", "Account");
            return View();
        }

        [HttpPost]
        public IActionResult Create(SystemAccount account)
        {
            if (!IsAdmin()) return RedirectToAction("Login", "Account");

            if (!ModelState.IsValid)
                return View(account);

            _accountService.Add(account);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Edit(SystemAccount account)
        {
            if (!IsAdmin()) return RedirectToAction("Login", "Account");

            if (!ModelState.IsValid)
                return View(account);

            _accountService.Update(account);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(short id)
        {
            if (!IsAdmin()) return RedirectToAction("Login", "Account");

            var account = _accountService.GetAccountById(id);
            if (account == null) return NotFound();

            return View(account);
        }

        public IActionResult Delete(short id)
        {
            if (!IsAdmin()) return RedirectToAction("Login", "Account");

            var account = _accountService.GetAccountById(id);
            if (account == null) return NotFound();

            return View(account);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(short id)
        {
            if (!IsAdmin()) return RedirectToAction("Login", "Account");

            _accountService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
        private bool IsAdmin()
        {
            return HttpContext.Session.GetInt32("ROLE") == 0;
        }
        private bool isStaff()
        {
            return HttpContext.Session.GetInt32("ROLE") == 1;

        }
        private bool isLecturer()
        {
            return HttpContext.Session.GetInt32("ROLE") == 2;
        }
    }
}
