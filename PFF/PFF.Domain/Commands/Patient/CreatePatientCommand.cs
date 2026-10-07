using PFF.Tools.CommandQuerySeparation;

namespace PFF.Domain.Commands.Patient
{
    public record CreatePatientCommand(
        string? SSIN,
        string? IdNumber,
        string? FirstName,
        string? LastName,
        string Alias,
        string? Gender,
        DateTime BirthDate,
        string? PhoneNumber,
        string? Allergies,
        bool? IsInsured,
        string? Insurance,
        bool? HasInsuranceCard,
        DateTime? InsuranceCardEndDate,
        bool? IsAtFedasil,
        int? Income,
        string? Status,
        bool? IsWorking,
        string? DrugType,
        string? ConsumptionFrequency,
        DateTime RegistrationDate,
        DateTime LastVisit,
        string? Street,
        string? Number,
        string ZipCode,
        string? Town,
        string? Country) : ICommandDefinition
    {
    }
}