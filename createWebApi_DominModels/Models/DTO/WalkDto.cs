namespace createWebApi_DominModels.Models.DTO
{
    public class WalkDto
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public double LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }

        //改成使用下面取得相關資料表的數據，而不是取得 Guid
        //public Guid DifficultyId { get; set; }
        //public Guid RegionId { get; set; }

        //回傳相關資料表的數據給使用者，
        public RegionDto? Region { get; set; }

        public DifficultyDto? Difficulty { get; set; }

    }
}
