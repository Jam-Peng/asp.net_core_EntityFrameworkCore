using createWebApi_DominModels.Data;
using createWebApi_DominModels.Models.Domain;

namespace createWebApi_DominModels.Repositories
{
    public class LoaclImageRepository : IImageRepository
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly WebApiSampleDbContext dbContext;


        /// <summary>
        /// 在建構函式中建立一個網路主機環境變數，為了要取得Image資料夾的路徑存放上傳的照片
        /// </summary>
        /// <param name="webHostEnvironment"></param>
        public LoaclImageRepository(IWebHostEnvironment webHostEnvironment,
            IHttpContextAccessor httpContextAccessor,
            WebApiSampleDbContext dbContext)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
            this.dbContext = dbContext;
        }


        public async Task<Image> Upload(Image image)
        {
            var localFilePath = Path.Combine(webHostEnvironment.ContentRootPath, "Images",
                $"{image.FileName}{image.FileExtension}");

            //上傳圖片到本地圖片存放的路徑
            using var stream = new FileStream(localFilePath, FileMode.Create);
            await image.File.CopyToAsync(stream);


            //要組出一個圖片的網址路徑 例: https://localhost:1234/Images/image.jpg
            var urlFilePath = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}{httpContextAccessor.HttpContext.Request.PathBase}/Images/{image.FileName}{image.FileExtension}";
            
            image.FilePath = urlFilePath;


            //將圖片儲存到資料庫
            await dbContext.Images.AddAsync(image);
            await dbContext.SaveChangesAsync();

            return image;
        }
    }
}
