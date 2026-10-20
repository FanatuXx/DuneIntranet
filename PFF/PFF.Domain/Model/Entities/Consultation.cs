using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PFF.Domain.Model.Entities
{
    public class Consultation
    {
        public int Id { get; set; }
        public string Motive { get; set; } = default!;
        public string? Subjective { get; set; } = default!;
        public string? Objective { get; set; } = default!;
        public string? Assessment { get; set; } = default!;
        public string? Plan { get; set; } = default!;
        public string? Description { get; set; } = default!;
        public DateTime Date { get; set; }
        public int PatientId { get; set; }
        public virtual Patient Patient { get; set; } = null!;
        public virtual IList<Worker> Workers { get; set; } = new List<Worker>();
        public virtual IList<Prescription>? Prescriptions { get; set; } = new List<Prescription>();
        public virtual IList<VitalSign>? VitalSigns { get; set; } = new List<VitalSign>();

        public virtual IList<Pathology> Pathologies { get; set; } = new List<Pathology>();
    }
}
