namespace DMS.az.Areas.Admin.ViewModels.Video
{
    public class VideoIndexVM
    {
        public VideoIndexVM()
        {
            Video = new List<Models.Video>();
        }
        public List<Models.Video> Video { get; set; }
    }
}
