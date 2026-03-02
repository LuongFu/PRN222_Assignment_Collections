using BusinessObjects;
using DataAccessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class NewsArticleRepository : INewsArticleRepository
    {
        private readonly NewsArticleDAO _newsArticleDAO;

        // Inject DAO
        public NewsArticleRepository(NewsArticleDAO newsArticleDAO)
        {
            _newsArticleDAO = newsArticleDAO;
        }

        public IEnumerable<NewsArticle> GetAll()
        {
            return _newsArticleDAO.GetAll();
        }

        public NewsArticle GetById(string id)
        {
            return _newsArticleDAO.GetById(id);
        }

        public void Add(NewsArticle news)
        {
            _newsArticleDAO.Add(news);
        }

        public void Update(NewsArticle news)
        {
            _newsArticleDAO.Update(news);
        }

        public void Delete(string id)
        {
            _newsArticleDAO.Delete(id);
        }
        public IEnumerable<NewsArticle> GetReportByPeriod(DateTime start, DateTime end)
        {
            return _newsArticleDAO.GetReportByPeriod(start, end);
        }
        public void Approve(string id, short adminId)
        {
            _newsArticleDAO.Approve(id, adminId);
        }
    }
}
