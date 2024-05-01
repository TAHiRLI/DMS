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
        public string? Search { get; set; }
        public IPagedList<Blog> Blogs { get; set; }

    }
}
