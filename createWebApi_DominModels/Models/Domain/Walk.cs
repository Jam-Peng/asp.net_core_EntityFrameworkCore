namespace createWebApi_DominModels.Models.Domain
{
    public class Walk
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public double LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }
        public Guid DifficultyId { get; set; }
        public Guid RegionId { get; set; }

 
        /// <summary>
        /// 關聯(連接 Foreign Key) difficulty.cs 難易度資料表
        /// </summary>
        public required Difficulty Difficulty { get; set; }

        /// <summary>
        /// 關聯(連接 Foreign Key) Region.cs 區域資料表
        /// </summary>
        public required Region Region { get; set; }

    }
}
