using System.ComponentModel.DataAnnotations;

namespace PFF.Api.Dtos
{
    public class RefreshTokenDto
    {
        [Required]
        public string Token { get; set; } = default!;
        [Required]
        public string RefreshToken { get; set; } = default!;
    }
}
