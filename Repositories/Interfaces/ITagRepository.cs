using BusinessObjects;
using System.Collections.Generic;

namespace Repositories
{
    public interface ITagRepository
    {
        IEnumerable<Tag> GetAll();
        List<Tag> GetTagsByIds(List<int> tagIds);
    }
}
