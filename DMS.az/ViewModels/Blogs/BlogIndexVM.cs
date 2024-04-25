using DMS.az.Models;
using X.PagedList;

namespace DMS.az.ViewModels.Blogs
{
    public class BlogIndexVM : PaginationVM
    {
        //public BlogIndexVM()
        //{
        //    Blogs = new List<Blog>();
        //}
        public IPagedList<Blog> Blogs { get; set; }

    }
}
