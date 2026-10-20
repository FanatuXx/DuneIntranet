using System.ComponentModel.DataAnnotations;

namespace PFF.Api.Dtos.Create
{
    public class CreatePathologyDto
    {
        [Required]
        [StringLength(128, MinimumLength = 1)]
        public string Name { get; set; } = default!;
    }
}
