using System.ComponentModel.DataAnnotations;

namespace createWebApi_DominModels.Models.DTO
{
    public class UpdateWalkRequestDto
    {
        [Required]
        [MaxLength(100)]
        public required string Name { get; set; }

        [Required]
        [MaxLength(500)]
        public required string Description { get; set; }

        [Required]
        [Range(0, 50)]
        public double LengthInKm { get; set; }

        public string? WalkImageUrl { get; set; }

        [Required]
        public Guid DifficultyId { get; set; }

        [Required]
        public Guid RegionId { get; set; }
    }
}
