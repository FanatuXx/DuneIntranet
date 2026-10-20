using PFF.Domain.Model.Entities;

namespace PFF.Api.Dtos.Update
{
    public class UpdateConsultationDto
    {
        public int Id { get; set; }
        public string? Motive { get; set; }
        public string? Subjective { get; set; }
        public string? Objective { get; set; }
        public string? Assessment { get; set; }
        public string? Plan { get; set; }
        public string? Description { get; set; }
        public DateTime? Date { get; set; }
        public int? PatientId { get; set; }
        public virtual Patient? Patient { get; set; } 
        public virtual IList<Worker>? Workers { get; set; } 
        public virtual IList<Prescription>? Prescriptions { get; set; }
        public virtual IList<VitalSign>? VitalSigns { get; set; }

        public virtual IList<Pathology>? Pathologies { get; set; }
    }
}
