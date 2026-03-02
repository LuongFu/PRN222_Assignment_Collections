using BusinessObjects;
using Microsoft.EntityFrameworkCore;

namespace DataAccessObjects
{
    public class NewsArticleDAO
    {
        private readonly FunewsManagementContext _context;

        public NewsArticleDAO(FunewsManagementContext context)
        {
            _context = context;
        }

        public IEnumerable<NewsArticle> GetAll()
        {
            return _context.NewsArticles
                           .Include(n => n.Category)
                           .Include(n => n.CreatedBy)
                           .Include(n => n.Tags)
                           .ToList();
        }
        public IEnumerable<NewsArticle> GetNewsByCatId(short categoryID)
        {
            return _context.NewsArticles
                .Include(n => n.Category)
                .Include(n => n.CreatedBy)
                .Include(n => n.Tags)
                .Where(n => n.CategoryId == categoryID)
                .ToList();
        }

        public NewsArticle GetById(string id)
        {
            return _context.NewsArticles
                .Include(n => n.Category)
                .Include(n => n.CreatedBy)
                .Include(n => n.Tags)
                .FirstOrDefault(n => n.NewsArticleId == id);
        }

        public void Add(NewsArticle news)
        {
            _context.NewsArticles.Add(news);
            _context.SaveChanges();
        }

        public void Update(NewsArticle news)
        {
            _context.NewsArticles.Update(news);
            _context.SaveChanges();
        }

        public void Delete(string id)
        {
            var news = _context.NewsArticles.Find(id);
            if (news != null)
            {
                _context.NewsArticles.Remove(news);
                _context.SaveChanges();
            }
        }
        public IEnumerable<NewsArticle> GetReportByPeriod(DateTime start, DateTime end)
        {
            return _context.NewsArticles
                           .Include(n => n.Category)
                            .Include(n => n.CreatedBy)
                            .Where(n => n.CreatedDate >= start && n.CreatedDate <= end)
                            .OrderByDescending(n => n.CreatedDate)
                            .ToList();
        }
        public void Approve(string id, short adminId)
        {
            var news = _context.NewsArticles.Find(id);
            if (news != null)
            {
                news.NewsStatus = true;
                news.UpdatedById = adminId;
                news.ModifiedDate = DateTime.Now;
                _context.Update(news);
                _context.SaveChanges();
            }
        }
    }
}
