namespace createWebApi_DominModels.Models.DTO
{
    public class DifficultyDto
    {
        public Guid Id { get; set; } //Guid 和 uuid 是一樣的
        public required string Name { get; set; }
    }
}
