﻿using createWebApi_DominModels.Data;
using createWebApi_DominModels.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace createWebApi_DominModels.Repositories
{
    /// <summary>
    /// SQL儲存庫模式 - Walks
    /// </summary>
    public class SQLWalkRepository : IWalkRepository
    {
        private readonly WebApiSampleDbContext dbContext;

        public SQLWalkRepository(WebApiSampleDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        
        // 建立單筆資料
        public async Task<Walk> CreateAsync(Walk walk)
        {
            await dbContext.Walks.AddAsync(walk);
            await dbContext.SaveChangesAsync();
            return walk;
        }


        // 取全部資料【使用Include("資料表") 函式，將相關資料表的資料取回】
        public async Task<List<Walk>> GetAllAsync()
        {
            return await dbContext.Walks.Include("Difficulty").Include("Region").ToListAsync();
        }


        // 取單一筆資料
        public async Task<Walk?> GetByIdAsync(Guid id)
        {
            return await dbContext.Walks
                .Include("Difficulty")
                .Include("Region")
                .FirstOrDefaultAsync(walk => walk.Id == id);
        }


        // 更新一筆資料
        public async Task<Walk?> UpdateAsync(Guid id, Walk walk)
        {
            var existWalk = await dbContext.Walks.FirstOrDefaultAsync(walk => walk.Id == id);

            if (existWalk == null)
            {
                return null;
            }

            existWalk.Name = walk.Name;
            existWalk.Description = walk.Description;
            existWalk.LengthInKm = walk.LengthInKm;
            existWalk.WalkImageUrl = walk.WalkImageUrl;
            existWalk.DifficultyId = walk.DifficultyId;
            existWalk.RegionId = walk.RegionId;

            await dbContext.SaveChangesAsync();
            return existWalk;
        }


        // 刪除一筆資料
        public async Task<Walk?> DeleteAsync(Guid id)
        {
            var existWalk = dbContext.Walks.FirstOrDefault(walk => walk.Id == id);

            if (existWalk == null)
            {
                return null;
            }

            dbContext.Walks.Remove(existWalk);
            await dbContext.SaveChangesAsync();
            return existWalk;
        }

    }
}