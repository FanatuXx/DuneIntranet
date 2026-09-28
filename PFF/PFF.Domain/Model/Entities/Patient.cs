using PFF.Domain.Model.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PFF.Domain.Model.Entities
{
    public class Patient
    {
        public int Id { get; set; }
        public string? SSIN { get; set; }
        public string? IdNumber { get; set; } 
        public string? FirstName { get; set; } 
        public string? LastName { get; set; } 
        public string? Alias { get; set; }
        public GenderEnum? Gender { get; set; }
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
        public DrugTypeEnum? DrugType { get; set; }
        public ConsumptionFrequencyEnum? ConsumptionFrequency { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime LastVisit { get; set; }
        public int? PatientAddressId { get; set; }
        public virtual PatientAddress? PatientAddress { get; set; } = null!;
        public virtual IList<Prescription> Prescriptions { get; set; } = new List<Prescription>();
        public virtual IList<Consultation> Consultations { get; set; } = new List<Consultation>();
        public virtual IList<ChronicTreatment> ChronicTreatments { get; set; } = new List<ChronicTreatment>();
    }
}
