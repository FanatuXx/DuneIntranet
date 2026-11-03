using System.ComponentModel.DataAnnotations;

namespace PFF.Api.Dtos.Update
{
    public class UpdateConsumptionDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 1)]
        public string? DrugType { get; set; }

        [Required]
        [StringLength(128, MinimumLength = 1)]
        public string? Frequency { get; set; }
    }
}
