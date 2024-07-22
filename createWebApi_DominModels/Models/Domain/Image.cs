using System.ComponentModel.DataAnnotations.Schema;

namespace createWebApi_DominModels.Models.Domain
{
    public class Image
    {
        public Guid Id { get; set; }

        [NotMapped]
        public required IFormFile File { get; set; }

        public required string FileName { get; set; }
        
        public string? FileDescription {  get; set; }

        /// <summary>
        /// 選擇副檔名是 jpg、png
        /// </summary>
        public string FileExtension { get; set; }

        public long FileSizeInBytes { get; set; }

        public string FilePath { get; set; }
    }
}
