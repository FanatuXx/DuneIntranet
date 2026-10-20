using PFF.Domain.Model.Entities;

namespace PFF.Api.Dtos.Create
{
    public class CreateConsultationDto
    {
        public int Id { get; set; }
        public string Motive { get; set; } = default!;
        public string? Subjective { get; set; }
        public string? Objective { get; set; }
        public string? Assessment { get; set; }
        public string? Plan { get; set; }
        public string? Description { get; set; }
        public DateTime Date { get; set; }
        public int PatientId { get; set; }
        public virtual Patient Patient { get; set; } = null!;
        public virtual IList<Worker> Workers { get; set; } = new List<Worker>();
        public virtual IList<Prescription>? Prescriptions { get; set; }
        public virtual IList<VitalSign>? VitalSigns { get; set; }

        public virtual IList<Pathology> Pathologies { get; set; } = new List<Pathology>();
    }
}
