using System.ComponentModel.DataAnnotations;

namespace PFF.Api.Dtos
{
    public class CreatePathologyDto
    {
        [Required]
        [StringLength(128, MinimumLength = 1)]
        public string Name { get; set; }
    }
}
