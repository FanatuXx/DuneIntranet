using PFF.Domain.Model.Entities;
using System.ComponentModel.DataAnnotations;

namespace PFF.Api.Dtos.Update
{
    public class UpdatePrescriptionDto
    {
        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        [StringLength(256, MinimumLength = 1)]
        public string? TakeFrequency { get; set; } = default!;

        public int? ConsultationId { get; set; }

        public int? PatientId { get; set; }

        public int? WorkerId { get; set; }

        public virtual IList<Medicine>? Medicines { get; set; }
    }
}
