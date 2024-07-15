using createWebApi_DominModels.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace createWebApi_DominModels.Data
{
    public class WebApiSampleDbContext : DbContext
    {
        //public WebApiSampleDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        public WebApiSampleDbContext(DbContextOptions<WebApiSampleDbContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        /// <summary>
        /// 定義資料表模型 (要建立的三個資料表)
        /// </summary>
        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }


        /// <summary>
        /// 建立預設資料到資料表中
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //為Difficulties 難易度資料表，定義預設列表數據(Easy, Medium, Hard)
            var difficulties = new List<Difficulty>()
            {
                new Difficulty()
                {
                    //按下檢視 > 選擇其他視窗> 與c#互動 > 使用Guid.NewGuid()產生uuid
                    Id = Guid.Parse("c6105626-2ed0-4f01-ba66-e9f8943e3c3c"),
                    Name = "Easy"
                },
                new Difficulty()
                {
                    Id = Guid.Parse("19c49e0c-ad0f-40a1-a446-c8baac4e4119"),
                    Name = "Medium"
                },
                new Difficulty()
                {
                    Id = Guid.Parse("f03ae466-c08f-4b85-80a9-22c55bcdaec7"),
                    Name = "Hard"
                }
            };

            modelBuilder.Entity<Difficulty>().HasData(difficulties);


            //為Region 資料表，定義一些數據
            var regions = new List<Region>
            {
                new Region 
                {
                    Id = Guid.Parse("b09924c5-a86d-4d5c-abd2-ede7587fcf25"),
                    Name = "Parko Parco 牛肚包義大利小酒館",
                    Code = "Parko",
                    RegionImageUrl = "https://angelababy.tw/wp-content/uploads/2022/01/DSC09440.jpg"
                },
                new Region 
                {
                    Id = Guid.Parse("2776d88d-b0c7-467c-ad85-1eb730e083f0"),
                    Name = "金孫韓廚義大利麵",
                    Code = "GSKP",
                    RegionImageUrl = "https://upssmile.com/wp-content/uploads/2022/12/20221205-IMG_5577.jpg"
                },
                new Region 
                {
                    Id = Guid.Parse("5351b315-43b3-4f61-83ec-c044efd9a650"),
                    Name = "Coco Brother 椰兄",
                    Code = "Coco",
                    RegionImageUrl = "https://leelife.tw/wp-content/uploads/2023/04/S__10117148.jpg"
                },
            };

            modelBuilder.Entity<Region>().HasData(regions);
        }
    }
}
