using PFF.Domain.Model.Entities;
using System.ComponentModel.DataAnnotations;

namespace PFF.Api.Dtos.Create
{
    public class CreateConsultationDto
    {
        [Required]
        [StringLength(128, MinimumLength = 1)]
        public string Motive { get; set; } = default!;

        [StringLength(128, MinimumLength = 1)]
        public string? Subjective { get; set; }

        [StringLength(128, MinimumLength = 1)]
        public string? Objective { get; set; }

        [StringLength(128, MinimumLength = 1)]
        public string? Assessment { get; set; }

        [StringLength(128, MinimumLength = 1)]
        public string? Plan { get; set; }

        [StringLength(128, MinimumLength = 1)]
        public string? Description { get; set; }

        [Required]
        public DateTime Date { get; set; } = DateTime.Now;

        [Required]
        public int PatientId { get; set; }


        [Required]
        public IList<int> WorkersIds { get; set; } = new List<int>();

        [Required]
        public IList<int> PathologiesIds { get; set; } = new List<int>();


        public IList<CreatePrescriptionDto>? Prescriptions { get; set; } 
        public IList<CreateVitalSignDto>? VitalSigns { get; set; }
    }
}
