using X.PagedList;

namespace DMS.az.Utilities.Pagination
{
    public class Paginator : IPaginator
    {
        public IPagedList<T>? GetPagedList<T>(ICollection<T> listUnpaged, int page = 1, int pageSize = 1)
        {
            var listPaged = listUnpaged.ToPagedList(page, pageSize);

            if (listPaged.PageNumber != 1 && page > listPaged.PageCount)
                return null;

            return listPaged;
        }
    }
}
