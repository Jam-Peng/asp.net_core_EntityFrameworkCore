using createWebApi_DominModels.Models.Domain;

namespace createWebApi_DominModels.Repositories
{
    public interface IWalkRepository
    {
        // 建立單筆資料
        Task<Walk> CreateAsync(Walk walk);

        // 取全部資料
        // Task<List<Walk>> GetAllAsync();
        //改成可篩選資料的方式
        Task<List<Walk>> GetAllAsync(string? filterOn = null, string? filterQuery = null);

        // 取單筆資料
        Task<Walk?> GetByIdAsync(Guid id);

        // 更新單筆資料
        Task<Walk?> UpdateAsync(Guid id, Walk walk);

        // 刪除單筆資料
        Task<Walk?> DeleteAsync(Guid id);
    }
}
