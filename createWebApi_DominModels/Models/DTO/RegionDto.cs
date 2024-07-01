namespace createWebApi_DominModels.Models.DTO
{
    //取得資料時的 DTO 類別物件
    public class RegionDto
    {
        public Guid Id { get; set; }
        public required string Code { get; set; }
        public required string Name { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}
