


using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Webp;
using SixLabors.ImageSharp.Processing;

namespace DSM.az.Utilities


{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FileService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<string>  Upload(IFormFile file)
        {
            var fileName = Guid.NewGuid() + "_" + file.FileName+".webp";
            var path = Path.Combine(_webHostEnvironment.WebRootPath, "Users/images", fileName);

            

            using (var image = Image.Load(file.OpenReadStream()))
            {
              
                var encoder = new WebpEncoder
                {
                    Quality = 75
                };

                var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "Users/images", fileName);


                // Save the image in WebP format
                await using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await image.SaveAsWebpAsync(fileStream, encoder);
                }
            }
            return fileName;
        }
        public void Delete(string photoName)
        {
            var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "Users/images", photoName);

            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
        }

        public bool IsImage(IFormFile file)
        {
            if (file.ContentType.Contains("image/")) return true;

            return false;
        }
        public bool IsVideo(IFormFile file)
        {
            if (file.ContentType.Contains("video/")) return true;

            return false;
        }

        public bool IsBiggerThanSize(IFormFile file, int size = 2000)
        {
            if (file.Length / 1024 < size) return true;

            return false;
        }
    }
}
