using DMS.az.Models;

namespace DMS.az.ViewModels.Blogs
{
    public class BlogIndexVM
    {
        public BlogIndexVM()
        {
            Blogs = new List<Blog>();
        }
        public List<Blog> Blogs { get; set; }
    }
}
