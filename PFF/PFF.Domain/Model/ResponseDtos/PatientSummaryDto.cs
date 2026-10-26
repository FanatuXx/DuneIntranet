namespace PFF.Domain.Model.ResponseDtos
{
    public class PatientSummaryDto
    {
        public int Id { get; set; }
        public string? SSIN { get; set; }
        public string? IdNumber { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string Alias { get; set; } = default!;
        public string? Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Allergies { get; set; }
        public bool? IsInsured { get; set; } = false;
        public string? Insurance { get; set; }
        public bool? HasInsuranceCard { get; set; } = false;
        public DateTime? InsuranceCardEndDate { get; set; }
        public bool? IsAtFedasil { get; set; } = false;
        public int? Income { get; set; }
        public string? Status { get; set; }
        public bool? IsWorking { get; set; } = false;
        public string? DrugType { get; set; }
        public string? ConsumptionFrequency { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime LastVisit { get; set; }
    }
}
