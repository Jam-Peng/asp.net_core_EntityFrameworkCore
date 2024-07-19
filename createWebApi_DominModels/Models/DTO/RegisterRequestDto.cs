using System.ComponentModel.DataAnnotations;

namespace createWebApi_DominModels.Models.DTO
{
    public class RegisterRequestDto
    {
        [Required]
        [DataType(DataType.EmailAddress)]

        public required string Username {  get; set; }

        [Required]
        [DataType(DataType.Password)]
        public required string Password { get; set; }

        public required string[] Roles { get; set; } //建立的使用者權限(Reader 或 Writer)
    }
}
