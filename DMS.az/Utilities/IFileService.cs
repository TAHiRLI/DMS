namespace DSM.az.Utilities
{
    public interface IFileService
    {
         Task<string> Upload(IFormFile file, string folderPath );
        void Delete(string photoName);
        bool IsImage(IFormFile file);
        bool IsVideo(IFormFile file);
        bool IsBiggerThanSize(IFormFile file, int size = 100);
    }
}
