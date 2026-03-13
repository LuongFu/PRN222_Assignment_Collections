using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Repositories;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LuongMinhPhuMVC.Controllers
{
    public class NewsArticlesController : Controller
    {
        private readonly INewsArticleService _newsService;
        private readonly ICategoryService _categoryService;
        private readonly ITagRepository _tagRepository;
        private readonly CloudinaryService _cloudinaryService;
        private readonly IHubContext<NewsHub.NewsHub> _hub;

        public NewsArticlesController(INewsArticleService newsService, CloudinaryService cloudinaryService, ICategoryService categoryService, ITagRepository tagRepository, IHubContext<NewsHub.NewsHub> hub)
        {
            _newsService = newsService;
            _categoryService = categoryService;
            _tagRepository = tagRepository;
            _cloudinaryService = cloudinaryService;
            _hub = hub;
        }

        // GET: NewsArticles
        public IActionResult Index()
        {
            ViewBag.CategoryId = new SelectList(_categoryService.GetCategories(), "CategoryId", "CategoryName");
            ViewBag.Tags = new MultiSelectList(_tagRepository.GetAll(), "TagId", "TagName");
            
            var articles = _newsService.GetAll();
            return View(articles);
        }

        // GET: NewsArticles/Details/5
        public IActionResult Details(string id)
        {
            if (id == null) return NotFound();

            var newsArticle = _newsService.GetById(id);
            if (newsArticle == null) return NotFound();

            return View(newsArticle);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NewsTitle,Headline,NewsContent,NewsSource,CategoryId")] NewsArticle newsArticle, List<int> selectedTags, IFormFile imageFile)
        {
            if (!IsStaffOrLecturer()) return RedirectToAction("Login", "Account");

            var userId = HttpContext.Session.GetInt32("USERID");
            if (userId == null) return RedirectToAction("Login", "Account");

            if (imageFile != null)
            {
                newsArticle.ImageUrl = await _cloudinaryService.UploadImageAsync(imageFile);
            }

            if (ModelState.IsValid)
            {
                newsArticle.NewsArticleId = Guid.NewGuid().ToString().Substring(0, 20);
                newsArticle.ImageUrl = newsArticle.ImageUrl ?? string.Empty;
                newsArticle.CreatedDate = DateTime.Now;
                newsArticle.NewsStatus = false;
                newsArticle.CreatedById = (short)userId.Value;

                if (selectedTags != null && selectedTags.Any())
                {
                    newsArticle.Tags = _tagRepository.GetTagsByIds(selectedTags);
                }

                _newsService.Add(newsArticle);
                await _hub.Clients.All.SendAsync("ReloadNews");
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Tags = new MultiSelectList(_tagRepository.GetAll(), "TagId", "TagName", selectedTags);
            ViewData["CategoryId"] = new SelectList(_categoryService.GetCategories(), "CategoryId", "CategoryName", newsArticle.CategoryId);

            return View(newsArticle);
        }



        public IActionResult Create()
        {
            if (!IsStaffOrLecturer()) return RedirectToAction("Login", "Account");

            ViewData["CategoryId"] = new SelectList(_categoryService.GetCategories(), "CategoryId", "CategoryName");
            ViewBag.Tags = new MultiSelectList(_tagRepository.GetAll(), "TagId", "TagName");

            return View();
        }

        public IActionResult Edit(string id)
        {
            if (id == null) return NotFound();

            var newsArticle = _newsService.GetById(id);
            if (newsArticle == null) return NotFound();

            ViewBag.Tags = new MultiSelectList(_tagRepository.GetAll(), "TagId", "TagName", newsArticle.Tags.Select(t => t.TagId));
            ViewData["CategoryId"] = new SelectList(_categoryService.GetCategories(), "CategoryId", "CategoryName", newsArticle.CategoryId);
            
            return View(newsArticle);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("NewsArticleId,NewsTitle,Headline,NewsContent,NewsSource,CategoryId")] NewsArticle newsArticle, List<int> selectedTags)
        {
            var userId = HttpContext.Session.GetInt32("USERID");
            var existing = _newsService.GetById(id);
            if (existing == null) return NotFound();

            if (!IsAdmin() && (existing.NewsStatus == true || existing.CreatedById != userId))
                return Forbid();

            if (ModelState.IsValid)
            {
                existing.NewsTitle = newsArticle.NewsTitle;
                existing.Headline = newsArticle.Headline;
                existing.NewsContent = newsArticle.NewsContent;
                existing.NewsSource = newsArticle.NewsSource;
                existing.CategoryId = newsArticle.CategoryId;
                existing.ModifiedDate = DateTime.Now;

                existing.Tags.Clear();
                if (selectedTags != null && selectedTags.Any())
                {
                    existing.Tags = _tagRepository.GetTagsByIds(selectedTags);
                }

                _newsService.Update(existing);
                await _hub.Clients.All.SendAsync("ReloadNews");
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Tags = new MultiSelectList(_tagRepository.GetAll(), "TagId", "TagName", selectedTags);
            ViewData["CategoryId"] = new SelectList(_categoryService.GetCategories(), "CategoryId", "CategoryName", newsArticle.CategoryId);

            return View(newsArticle);
        }


        public IActionResult Delete(string id)
        {
            if (id == null) return NotFound();

            var newsArticle = _newsService.GetById(id);
            if (newsArticle == null) return NotFound();

            return View(newsArticle);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmedAsync(string id)
        {
            var userId = HttpContext.Session.GetInt32("USERID");
            var existing = _newsService.GetById(id);
            if (existing == null) return NotFound();

            if (!IsAdmin() && (existing.NewsStatus == true || existing.CreatedById != userId))
                return Forbid();

            _newsService.Delete(id);
            await _hub.Clients.All.SendAsync("ReloadNews");
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Approve(string id)
        {
            if (!IsAdmin()) return RedirectToAction("Login", "Account");

            var adminId = (short)HttpContext.Session.GetInt32("USERID").Value;
            _newsService.Approve(id, adminId);
            await _hub.Clients.All.SendAsync("ReloadNews");
            return RedirectToAction(nameof(Index));
        }
        public IActionResult MyNews()
        {
            var role = HttpContext.Session.GetInt32("ROLE");
            var userId = HttpContext.Session.GetInt32("USERID");

            if (role != 1 || userId == null) return RedirectToAction("Index", "Home");

            var myNews = _newsService.GetAll()
                .Where(n => n.CreatedById == userId)
                .OrderByDescending(n => n.CreatedDate)
                .ToList();

            return View(myNews);
        }

        private bool IsAdmin()
        {
            return HttpContext.Session.GetInt32("ROLE") == 0;
        }

        private bool NewsArticleExists(string id)
        {
            return _newsService.GetById(id) != null;
        }
        private bool IsStaffOrLecturer()
        {
            var role = HttpContext.Session.GetInt32("ROLE");
            return role == 1 || role == 2;
        }
        private bool IsStaff()
        {
            return HttpContext.Session.GetInt32("ROLE") == 1;
        }
        private bool IsLecturer()
        {
            return HttpContext.Session.GetInt32("ROLE") == 2;
        }
    }
}
