using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace createWebApi_DominModels.Data
{
    public class WalksAuthDbContext: IdentityDbContext 
    {
        public WalksAuthDbContext(DbContextOptions<WalksAuthDbContext> options) : base(options) 
        {

        }


        /// <summary>
        /// 建立不同的使用者(可讀取、可寫入)的預設資料到資料表中
        /// </summary>
        /// <param name="builder"></param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var readerRoleId = "b2c76880-10ed-4342-896f-ca9f41a28eee";  //可讀取資料的使用者權限Id
            var writerRoleId = "45c6d434-8521-44dd-8c25-a7a564730cb4";  //可寫入資料的使用者權限Id

            //建立不同的使用者(可讀取、可寫入)
            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = readerRoleId,
                    ConcurrencyStamp = readerRoleId,
                    Name = "Reader",
                    NormalizedName = "Reader".ToUpper()
                },
                new IdentityRole
                {
                    Id = writerRoleId,
                    ConcurrencyStamp = writerRoleId,
                    Name = "Writer",
                    NormalizedName = "Writer".ToUpper()
                }
            };

            builder.Entity<IdentityRole>().HasData(roles);  //建立實體
        }


    }
}
