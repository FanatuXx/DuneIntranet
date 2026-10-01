using PFF.Tools.CommandQuerySeparation;
using System;
using System.Collections.Generic;
using System.Text;

namespace PFF.Domain.Commands
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
        string? ConsumptionFrequency) : ICommandDefinition
    {
    }
}
