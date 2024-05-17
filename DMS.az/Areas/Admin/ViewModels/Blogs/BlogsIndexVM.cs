using DMS.az.Models;

namespace DMS.az.Areas.Admin.ViewModels.Blogs
{
    public class BlogsIndexVM
    {
        public BlogsIndexVM()
        {
            Blogs = new List<Blog>();
        }
        public List<Blog> Blogs {  get; set; }

        public bool IsSent { get; set; }
    }
}
