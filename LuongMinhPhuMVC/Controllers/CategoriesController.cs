using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessObjects;
using Services;

namespace LuongMinhPhuMVC.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // GET: Categories
        public IActionResult Index()
        {
            var categories = _categoryService.GetCategories();
            return View(categories);
        }

        // GET: Categories/Details/5
        public IActionResult Details(short? id)
        {
            if (id == null) return NotFound();

            var category = _categoryService.GetCategoryById(id.Value);
            if (category == null) return NotFound();

            return View(category);
        }

        // GET: Categories/Create
        public IActionResult Create()
        {
            ViewData["ParentCategoryId"] = new SelectList(_categoryService.GetCategories(), "CategoryId", "CategoryName");
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("CategoryId,CategoryName,CategoryDesciption,ParentCategoryId,IsActive")] Category category)
        {
            if (ModelState.IsValid)
            {
                _categoryService.AddCategory(category);
                return RedirectToAction(nameof(Index));
            }
            ViewData["ParentCategoryId"] = new SelectList(_categoryService.GetCategories(), "CategoryId", "CategoryName", category.ParentCategoryId);
            return View(category);
        }

        // GET: Categories/Edit/5
        public IActionResult Edit(short? id)
        {
            if (id == null) return NotFound();

            var category = _categoryService.GetCategoryById(id.Value);
            if (category == null) return NotFound();

            ViewData["ParentCategoryId"] = new SelectList(_categoryService.GetCategories(), "CategoryId", "CategoryName", category.ParentCategoryId);
            return View(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(short id, [Bind("CategoryId,CategoryName,CategoryDesciption,ParentCategoryId,IsActive")] Category category)
        {
            if (id != category.CategoryId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _categoryService.UpdateCategory(category);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_categoryService.CategoryExists(category.CategoryId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ParentCategoryId"] = new SelectList(_categoryService.GetCategories(), "CategoryId", "CategoryName", category.ParentCategoryId);
            return View(category);
        }

        // GET: Categories/Delete/5
        public IActionResult Delete(short? id)
        {
            if (id == null) return NotFound();

            var category = _categoryService.GetCategoryById(id.Value);
            if (category == null) return NotFound();

            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(short id)
        {
            var category = _categoryService.GetCategoryById(id);

            if (category == null) return NotFound();

            if (category.NewsArticles != null && category.NewsArticles.Any())
            {
                TempData["Error"] = "Cannot delete. Category is used in News Articles.";
                return RedirectToAction(nameof(Index));
            }

            _categoryService.DeleteCategory(id);

            return RedirectToAction(nameof(Index));
        }

    }
}
