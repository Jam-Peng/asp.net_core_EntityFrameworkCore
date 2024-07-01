namespace createWebApi_DominModels.Models.DTO
{
    //建立資料時使用的 DTO 類別物件
    public class AddRegionRequestDto
    {
        public required string Code { get; set; }
        public required string Name { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}
