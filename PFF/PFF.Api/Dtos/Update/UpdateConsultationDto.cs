using PFF.Api.Dtos.Create;
using PFF.Domain.Model.Entities;
using System.ComponentModel.DataAnnotations;

namespace PFF.Api.Dtos.Update
{
    public class UpdateConsultationDto
    {
        [StringLength(256, MinimumLength = 1)]
        public string? Motive { get; set; }

        [StringLength(256, MinimumLength = 1)]
        public string? Subjective { get; set; }

        [StringLength(256, MinimumLength = 1)]
        public string? Objective { get; set; }

        [StringLength(256, MinimumLength = 1)]
        public string? Assessment { get; set; }

        [StringLength(256, MinimumLength = 1)]
        public string? Plan { get; set; }

        [StringLength(256, MinimumLength = 1)]
        public string? Description { get; set; }

        public DateTime? Date { get; set; }



        public int? PatientId { get; set; }

        public IList<int>? WorkersIds { get; set; } = new List<int>();

        public IList<int>? PathologiesIds { get; set; } = new List<int>();



        public IList<UpdatePrescriptionDto>? PrescriptionsToUpdate { get; set; }
        public IList<CreatePrescriptionDto>? PrescriptionsToCreate { get; set; }


        public IList<UpdateVitalSignDto>? VitalSignsToUpdate { get; set; }
        public IList<CreateVitalSignDto>? VitalSignsToCreate { get; set; }
    }
}
