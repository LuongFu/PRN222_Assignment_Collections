using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LuongMinhPhuMVC.Controllers
{
    public class NewsArticlesController : Controller
    {
        private readonly FunewsManagementContext _context;
        private readonly INewsArticleService _newsService;
        private readonly CloudinaryService _cloudinaryService;

        public NewsArticlesController(FunewsManagementContext context, INewsArticleService newsService, CloudinaryService cloudinaryService)
        {
            _context = context;
            _newsService = newsService;
            _cloudinaryService = cloudinaryService;
        }

        // GET: NewsArticles
        public async Task<IActionResult> Index()
        {
            ViewBag.CategoryId = new SelectList(
        _context.Categories,
        "CategoryId",
        "CategoryName"
    );
            ViewBag.Tags = new MultiSelectList(
        _context.Tags.ToList(),
        "TagId",
        "TagName"
    );
            var funewsManagementContext = _context.NewsArticles
                .Include(n => n.Category)
                .Include(n => n.CreatedBy)
                .Include(n => n.Tags).ToListAsync();
            return View(await funewsManagementContext);
        }

        // GET: NewsArticles/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var newsArticle = await _context.NewsArticles
                .Include(n => n.Category)
                .Include(n => n.CreatedBy)
                .Include(n => n.Tags)
                .FirstOrDefaultAsync(m => m.NewsArticleId == id);
            if (newsArticle == null)
            {
                return NotFound();
            }

            return View(newsArticle);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
    [Bind("NewsTitle,Headline,NewsContent,NewsSource,CategoryId")]
    NewsArticle newsArticle, List<int> selectedTags, IFormFile imageFile)
        {
            if (!IsStaff()) return RedirectToAction("Login", "Account");

            var userId = HttpContext.Session.GetInt32("USERID");
            if (userId == null)
                return RedirectToAction("Login", "Account");

            if (imageFile != null)
            {
                var imageUrl = await _cloudinaryService.UploadImageAsync(imageFile);
                newsArticle.ImageUrl = imageUrl;
            }

            if (ModelState.IsValid)
            {
                newsArticle.NewsArticleId = Guid.NewGuid().ToString().Substring(0,20); // set string 20 chars
                newsArticle.ImageUrl = newsArticle.ImageUrl ?? string.Empty; // set empty string if null
                newsArticle.CreatedDate = DateTime.Now;
                newsArticle.NewsStatus = false; // pending
                newsArticle.CreatedById = (short)userId.Value;

                if (selectedTags != null && selectedTags.Any())
                {
                    var tags = await _context.Tags
                        .Where(t => selectedTags.Contains(t.TagId))
                        .ToListAsync();

                    newsArticle.Tags = tags;
                }

                _newsService.Add(newsArticle);

                return RedirectToAction(nameof(Index));
            }
            else
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine(error.ErrorMessage);
                }
            }

            ViewBag.Tags = new MultiSelectList(
                _context.Tags.ToList(),
                "TagId",
                "TagName",
                newsArticle.Tags.Select(t => t.TagId)
            );

            ViewData["CategoryId"] =
            new SelectList(_context.Categories,
                "CategoryId", "CategoryDesciption",
                newsArticle.CategoryId);

            return View(newsArticle);
        }



        public IActionResult Create()
        {
            if (!IsStaff()) return RedirectToAction("Login", "Account");

            ViewData["CategoryId"] =
                new SelectList(_context.Categories, "CategoryId", "CategoryDesciption");

            return View();
        }

        // GET: NewsArticles/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var newsArticle = await _context.NewsArticles.FindAsync(id);
            var newsArticle = await _context.NewsArticles
            .Include(n => n.Tags)
            .FirstOrDefaultAsync(n => n.NewsArticleId == id);

            if (newsArticle == null)
            {
                return NotFound();
            }
            ViewBag.Tags = new MultiSelectList(
                _context.Tags.ToList(),
                "TagId",
                "TagName"
            );

            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryDesciption", newsArticle.CategoryId);
            ViewData["CreatedById"] = new SelectList(_context.SystemAccounts, "AccountId", "AccountId", newsArticle.CreatedById);
            return View(newsArticle);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
    string id,
    [Bind("NewsArticleId,NewsTitle,Headline,NewsContent,NewsSource,CategoryId")]
    NewsArticle newsArticle, List<int> selectedTags)
        {
            if (!IsStaff()) return RedirectToAction("Login", "Account");

            //var existing = await _context.NewsArticles.FindAsync(id);
            var existing = await _context.NewsArticles
            .Include(n => n.Tags)
            .FirstOrDefaultAsync(n => n.NewsArticleId == id);

            if (existing == null) return NotFound();

            if (existing.NewsStatus == true)
                return Forbid();

            existing.Tags.Clear();

            if (selectedTags != null && selectedTags.Any())
            {
                var tags = await _context.Tags
                    .Where(t => selectedTags.Contains(t.TagId))
                    .ToListAsync();

                foreach (var tag in tags)
                {
                    existing.Tags.Add(tag);
                }
            }


            if (existing == null) return NotFound();

            if (existing.NewsStatus == true)
                return Forbid();

            if (ModelState.IsValid)
            {
                existing.NewsTitle = newsArticle.NewsTitle;
                existing.Headline = newsArticle.Headline;
                existing.NewsContent = newsArticle.NewsContent;
                existing.NewsSource = newsArticle.NewsSource;
                existing.CategoryId = newsArticle.CategoryId;
                existing.ModifiedDate = DateTime.Now;

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["CategoryId"] =
                new SelectList(_context.Categories,
                    "CategoryId", "CategoryName",
                    newsArticle.CategoryId);

            return View(newsArticle);
        }


        // GET: NewsArticles/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var newsArticle = await _context.NewsArticles
                .Include(n => n.Category)
                .Include(n => n.CreatedBy)
                .FirstOrDefaultAsync(m => m.NewsArticleId == id);
            if (newsArticle == null)
            {
                return NotFound();
            }

            return View(newsArticle);
        }

        // POST: NewsArticles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var news = await _context.NewsArticles
                .Include(n => n.Tags)
                .FirstOrDefaultAsync(n => n.NewsArticleId == id);

            if (news == null) return NotFound();
            
            news.Tags.Clear();

            _context.NewsArticles.Remove(news);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Approve(string id)
        {
            if (!IsAdmin()) return RedirectToAction("Login", "Account");

            var news = await _context.NewsArticles.FindAsync(id);
            if (news == null) return NotFound();

            news.NewsStatus = true;
            news.UpdatedById =
                (short?)HttpContext.Session.GetInt32("USERID").Value;
            news.ModifiedDate = DateTime.Now;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> MyNews()
        {
            var role = HttpContext.Session.GetInt32("ROLE");
            var userId = HttpContext.Session.GetInt32("USERID");

            if (role != 1 || userId == null)
                return RedirectToAction("Index", "Home");

            var myNews = await _context.NewsArticles
                .Where(n => n.CreatedById == userId)
                .Include(n => n.Category)
                .Include(n => n.Tags)
                .OrderByDescending(n => n.CreatedDate)
                .ToListAsync();

            return View(myNews);
        }

        private bool IsAdmin()
        {
            return HttpContext.Session.GetInt32("ROLE") == 0;
        }

        private bool NewsArticleExists(string id)
        {
            return _context.NewsArticles.Any(e => e.NewsArticleId == id);
        }
        private bool IsStaff()
        {
            return HttpContext.Session.GetInt32("ROLE") == 1;
        }
    }
}
