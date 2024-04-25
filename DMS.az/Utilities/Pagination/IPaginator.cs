using X.PagedList;

namespace DMS.az.Utilities.Pagination
{
    public interface IPaginator
    {
        public IPagedList<T> GetPagedList<T>(ICollection<T> listUnpaged, int currentPage = 1, int pageSize = 1);

    }
}
