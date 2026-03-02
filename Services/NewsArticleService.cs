using BusinessObjects;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class NewsArticleService : INewsArticleService
    {
        private readonly INewsArticleRepository _newsArticleRepo;
        public NewsArticleService(INewsArticleRepository newsArticleRepo)
        {
            _newsArticleRepo = newsArticleRepo;
        }

        public void Add(NewsArticle news)
        {
            _newsArticleRepo.Add(news);
        }

        public void Delete(string id)
        {
            _newsArticleRepo.Delete(id);
        }

        public IEnumerable<NewsArticle> GetAll()
        {
            return _newsArticleRepo.GetAll();
        }

        public NewsArticle GetById(string id)
        {
            return _newsArticleRepo.GetById(id);
        }

        public void Update(NewsArticle news)
        {
            _newsArticleRepo.Update(news);
        }
        public IEnumerable<NewsArticle> GetReportByPeriod(DateTime start, DateTime end)
        {
            return _newsArticleRepo.GetReportByPeriod(start, end);
        }
        public void Approve(string id, short adminId)
        {
            _newsArticleRepo.Approve(id, adminId);
        }
    }
}
