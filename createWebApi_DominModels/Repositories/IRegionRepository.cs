using createWebApi_DominModels.Models.Domain;
using System;

namespace createWebApi_DominModels.Repositories
{
    public interface IRegionRepository
    {
        // 取全部資料
        Task<List<Region>> GetAllAsync();

        // 取單筆資料
        Task<Region?> GetByIdAsync(Guid id);

        // 建立單筆資料
        Task<Region> CreateAsync(Region region);

        // 更新單筆資料 ?是指有進行查找是否有該筆資料的判斷
        Task<Region?> UpdateAsync(Guid id, Region region);

        // 刪除單筆資料
        Task<Region?> DeleteAsync(Guid id);
    }
}
