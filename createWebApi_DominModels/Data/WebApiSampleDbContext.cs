using createWebApi_DominModels.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace createWebApi_DominModels.Data
{
    public class WebApiSampleDbContext : DbContext
    {
        public WebApiSampleDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }

        // 定義資料表模型 (要建立的三個資料表)
        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }

    }
}
