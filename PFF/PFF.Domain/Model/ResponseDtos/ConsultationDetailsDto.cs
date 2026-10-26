namespace PFF.Domain.Model.ResponseDtos
{
    public class ConsultationDetailsDto
    {
        public int Id { get; set; }
        public string Motive { get; set; } = default!;
        public string? Subjective { get; set; }
        public string? Objective { get; set; }
        public string? Assessment { get; set; }
        public string? Plan { get; set; }
        public string? Description { get; set; }
        public DateTime Date { get; set; }

        public PatientSummaryDto? Patient { get; set; }

        public IList<WorkerSummaryDto> Workers { get; set; } = new List<WorkerSummaryDto>();
        public IList<PathologySummaryDto> Pathologies { get; set; } = new List<PathologySummaryDto>();
        public IList<VitalSignSummaryDto>? VitalSigns { get; set; }
        public IList<PrescriptionWithMedicineDto>? Prescriptions { get; set; }
    }
}