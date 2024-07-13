using System.ComponentModel.DataAnnotations;

namespace createWebApi_DominModels.Models.DTO
{
    //建立資料時使用的 DTO 類別物件
    public class AddRegionRequestDto
    {
        [Required]
        [MinLength(3, ErrorMessage = "最少必須輸入3個字元的代碼")]
        [MaxLength(10, ErrorMessage = "代碼超過10個字元的限制")]
        public required string Code { get; set; }

        [Required]
        [MaxLength(10, ErrorMessage = "名稱超過10個字元的限制")]
        public required string Name { get; set; }

        public string? RegionImageUrl { get; set; }
    }
}
