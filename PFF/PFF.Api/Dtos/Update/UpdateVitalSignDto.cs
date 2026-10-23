using System.ComponentModel.DataAnnotations;

namespace PFF.Api.Dtos.Update
{
    public class UpdateVitalSignDto
    {
        public int? HeartRate { get; set; }

        [StringLength(50, MinimumLength = 1)]
        public string? BloodPressure { get; set; }

        public decimal? Temperature { get; set; }
        public int? OxygenSaturation { get; set; }
        public int? Height { get; set; }
        public float? Weight { get; set; }
        public float? BloodSugar { get; set; }
        public DateTime? Date { get; set; }
    }
}
