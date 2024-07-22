using createWebApi_DominModels.Models.Domain;

namespace createWebApi_DominModels.Repositories
{
    public interface IImageRepository
    {
        Task<Image> Upload(Image image);
    }
}
