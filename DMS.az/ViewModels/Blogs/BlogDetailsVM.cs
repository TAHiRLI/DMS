namespace DMS.az.ViewModels.Blogs
{
    public class BlogDetailsVM
    {
        public BlogDetailsVM()
        {
            Blogs = new List<Models.Blog>();
        }
        public Models.Blog Blog { get; set; }
        public List<Models.Blog> Blogs { get; set; }
    }
}
