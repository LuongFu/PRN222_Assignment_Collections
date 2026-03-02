using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface INewsArticleService
    {
        IEnumerable<NewsArticle> GetAll();
        NewsArticle GetById(string id);
        void Add(NewsArticle news);
        void Update(NewsArticle news);
        void Delete(string id);
        IEnumerable<NewsArticle> GetReportByPeriod(DateTime start, DateTime end);
        void Approve(string id, short adminId);
    }
}
