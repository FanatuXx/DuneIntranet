namespace PFF.Domain.Model.ResponseDtos
{
    public class VitalSignSummaryDto
    {
        public int Id { get; set; }
        public int HeartRate { get; set; }
        public string? BloodPressure { get; set; } = default!;
        public decimal? Temperature { get; set; }
        public int OxygenSaturation { get; set; }
        public float? BMI { get; set; }
        public float? BloodSugar { get; set; }
    }
}

