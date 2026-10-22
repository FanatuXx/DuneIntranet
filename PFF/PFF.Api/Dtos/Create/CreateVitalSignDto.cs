using System.ComponentModel.DataAnnotations;

namespace PFF.Api.Dtos.Create
{
    public class CreateVitalSignDto
    {
        [Required]
        public int HeartRate { get; set; }

        [StringLength(50, MinimumLength = 1)]
        public string? BloodPressure { get; set; } = default!;

        public decimal? Temperature { get; set; }

        [Required]
        public int OxygenSaturation { get; set; }
        public int? Height { get; set; }
        public float? Weight { get; set; }
        public float? BloodSugar { get; set; }

        [Required]
        public DateTime Date { get; set; } = DateTime.Now;

        public int ConsultationId { get; set; }
    }
}
