using createWebApi_DominModels.Models.Domain;
using createWebApi_DominModels.Models.DTO;
using createWebApi_DominModels.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace createWebApi_DominModels.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository imageRepository;

        public ImagesController(IImageRepository imageRepository)
        {
            this.imageRepository = imageRepository;
        }


        /// <summary>
        /// 建立圖片
        /// POST: /api/Images/Upload
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> Upload([FromForm] ImageUploadRequestDto request)
        {
            //上傳圖片的驗證
            ValidateFileUpload(request);

            if(ModelState.IsValid)
            {
                //轉換成 DTO 模型
                var imageDomainModel = new Image 
                {   
                    File = request.File,
                    FileExtension = Path.GetExtension(request.File.FileName),
                    FileSizeInBytes = request.File.Length,
                    FileName = request.File.FileName,
                    FileDescription = request.FileDescription,
                };


                //圖片儲存至圖片資料夾和資料庫
                await imageRepository.Upload(imageDomainModel);

                return Ok(imageDomainModel);
            }

            return BadRequest(ModelState);
        }


        /// <summary>
        /// 上傳圖片的驗證 (驗證副檔名、檔案大小)
        /// </summary>
        /// <param name="request"></param>
        private void ValidateFileUpload(ImageUploadRequestDto request)
        {
            var allowedExtensions = new string[] { ".jpg", ".jpeg", ".png" };

            if(!allowedExtensions.Contains(Path.GetExtension(request.File.FileName)))
            {
                ModelState.AddModelError("file", "檔案格式不支援");
            }

            // 檔案大小要在 10MB之內
            if(request.File.Length > 10485760) 
            {
                ModelState.AddModelError("file", "檔案大小不能超過10MB");
            }
        }


    }
}
