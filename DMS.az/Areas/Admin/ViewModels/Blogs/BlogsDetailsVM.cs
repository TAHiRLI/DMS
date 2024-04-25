namespace DMS.az.Areas.Admin.ViewModels.Blogs
{
    public class BlogsDetailsVM
    {
        public string Photo { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ShortDesc { get; set; }
        public DateTime PostDate { get; set; }
        public int ViewCount { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set;}
    }
}
