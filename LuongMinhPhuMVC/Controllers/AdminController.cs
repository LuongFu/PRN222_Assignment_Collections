using BusinessObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services;

namespace LuongMinhPhuMVC.Controllers
{
    public class AdminController : Controller
    {
        private readonly INewsArticleService _newsService;
        public AdminController(INewsArticleService newsService)
        {
            _newsService = newsService;
        }
        // GET: AdminController
        public ActionResult Index()
        {
            return View();
        }

        // GET: AdminController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AdminController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AdminController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AdminController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AdminController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AdminController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        [HttpPost]
        public IActionResult Report(DateTime startDate, DateTime endDate)
        {
            if (!IsAdmin()) return RedirectToAction("Login", "Account");

            var reportData = _newsService.GetReportByPeriod(startDate, endDate);

            return View("ReportResult", reportData);
        }

        public IActionResult Report()
        {
            if (!IsAdmin()) return RedirectToAction("Login", "Account");
            return View();
        }
        public IActionResult Approve(string id)
        {
            if (!IsAdmin())
                return RedirectToAction("Login", "Account");

            var adminId = (short)HttpContext.Session.GetInt32("USERID").Value;
            _newsService.Approve(id, adminId);
            
            return RedirectToAction(nameof(Index));
        }

        private bool IsAdmin()
        {
            return HttpContext.Session.GetInt32("ROLE") == 0;
        }
    }
}
