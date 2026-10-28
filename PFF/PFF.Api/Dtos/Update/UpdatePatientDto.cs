using PFF.Api.Dtos.Create;
using PFF.Domain.Model.Entities;
using System.ComponentModel.DataAnnotations;

namespace PFF.Api.Dtos.Update
{
    public class UpdatePatientDto
    {
        public string? SSIN { get; set; }
        public string? IdNumber { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Alias { get; set; }
        public string? Gender { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Allergies { get; set; }
        public bool? IsInsured { get; set; }
        public string? Insurance { get; set; }
        public bool? HasInsuranceCard { get; set; }
        public DateTime? InsuranceCardEndDate { get; set; }
        public bool? IsAtFedasil { get; set; }
        public int? Income { get; set; }
        public string? Status { get; set; }
        public bool? IsWorking { get; set; }
        public IList<int>? ConsumptionsToDeleteIds { get; set; }

        public UpdatePatientAddressDto? PatientAddress { get; set; }

        public IList<UpdateConsumptionDto>? ConsumptionsToUpdate { get; set; }
        public IList<CreateConsumptionDto>? ConsumptionsToCreate { get; set; }
    }
}
