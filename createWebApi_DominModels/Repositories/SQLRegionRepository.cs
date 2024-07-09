using createWebApi_DominModels.Data;
using createWebApi_DominModels.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace createWebApi_DominModels.Repositories
{
    /// <summary>
    /// SQL儲存庫模式 - Regions
    /// </summary>
    public class SQLRegionRepository : IRegionRepository
    {
        private readonly WebApiSampleDbContext dbContext;

        public SQLRegionRepository(WebApiSampleDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // 取全部資料
        public async Task<List<Region>> GetAllAsync()
        {
            return await dbContext.Regions.ToListAsync();
        }

        // 取單一筆資料
        public async Task<Region?> GetByIdAsync(Guid id)
        {
            return await dbContext.Regions.FirstOrDefaultAsync(region => region.Id == id);
        }

        // 建立一筆資料
        public async Task<Region> CreateAsync(Region region)
        {
            await dbContext.Regions.AddAsync(region);
            await dbContext.SaveChangesAsync();
            return region;
        }

        // 更新一筆資料
        public async Task<Region?> UpdateAsync(Guid id, Region region)
        {
            var existRegion = await dbContext.Regions.FirstOrDefaultAsync(region => region.Id == id);
            
            if(existRegion == null)
            {
                return null;
            }

            existRegion.Code = region.Code;
            existRegion.Name = region.Name;
            existRegion.RegionImageUrl = region.RegionImageUrl;

            await dbContext.SaveChangesAsync();
            return existRegion;
        }

        // 刪除一筆資料
        public async Task<Region?> DeleteAsync(Guid id)
        {
            var existRegion = dbContext.Regions.FirstOrDefault(region => region.Id == id);

            if(existRegion == null)
            {
                return null;
            }

            dbContext.Regions.Remove(existRegion);
            await dbContext.SaveChangesAsync();
            return existRegion;
        }
    }
}
