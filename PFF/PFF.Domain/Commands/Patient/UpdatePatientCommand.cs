using PFF.Tools.CommandQuerySeparation;
using System;
using System.Collections.Generic;
using System.Text;

namespace PFF.Domain.Commands.Patient
{
    public record UpdatePatientCommand(
        int Id,
        string? SSIN,
        string? IdNumber,
        string? FirstName,
        string? LastName,
        string? Alias,
        string? Gender,
        DateTime? BirthDate,
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
        string? Street,
        string? Number,
        string? ZipCode,
        string? Town,
        string? Country) : ICommandDefinition

    {
    }
}
