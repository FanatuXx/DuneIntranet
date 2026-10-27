using System.ComponentModel.DataAnnotations;

namespace PFF.Api.Dtos.Create
{
    public class CreateConsumptionDto
    {
        [Required]
        [StringLength(50, MinimumLength = 1)]
        public string DrugType { get; set; } = default!;

        [Required]
        [StringLength(128, MinimumLength = 1)]
        public string Frequency { get; set; } = default!;
    }
}
