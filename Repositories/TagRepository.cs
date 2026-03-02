using BusinessObjects;
using DataAccessObjects;
using Repositories;
using System.Collections.Generic;

namespace Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly TagDAO _tagDAO;

        public TagRepository(TagDAO tagDAO)
        {
            _tagDAO = tagDAO;
        }

        public IEnumerable<Tag> GetAll()
        {
            return _tagDAO.GetAll();
        }

        public List<Tag> GetTagsByIds(List<int> tagIds)
        {
            return _tagDAO.GetTagsByIds(tagIds);
        }
    }
}
