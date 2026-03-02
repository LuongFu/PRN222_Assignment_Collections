using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DataAccessObjects
{
    public class TagDAO
    {
        private readonly FunewsManagementContext _context;

        public TagDAO(FunewsManagementContext context)
        {
            _context = context;
        }

        public IEnumerable<Tag> GetAll()
        {
            return _context.Tags.ToList();
        }

        public List<Tag> GetTagsByIds(List<int> tagIds)
        {
            return _context.Tags.Where(t => tagIds.Contains(t.TagId)).ToList();
        }
    }
}
