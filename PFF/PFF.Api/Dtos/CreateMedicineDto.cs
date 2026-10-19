using System.ComponentModel.DataAnnotations;

namespace PFF.Api.Dtos
{
    public class CreateMedicineDto
    {
        [Required]
        [StringLength(128, MinimumLength = 1)]
        public string Name { get; set; } = default!;
        [Required]
        [StringLength(128, MinimumLength = 1)]
        public string HowToTake { get; set; } = default!;
    }
}
