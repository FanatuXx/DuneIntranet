using PFF.Domain.Model.Entities;
using PFF.Domain.Model.Enum;
using PFF.Tools.CommandQuerySeparation;
using System;
using System.Collections.Generic;
using System.Text;

namespace PFF.Domain.Commands
{
    public record CreatePatientCommand(
        string? SSIN, 
        string? IdNumber, 
        string? FirstName, 
        string? LastName, 
        string? Alias, 
        GenderEnum? Gender, 
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
        DrugTypeEnum? DrugType,
        ConsumptionFrequencyEnum? ConsumptionFrequency,
        DateTime RegistrationDate,
        DateTime LastVisit) : ICommandDefinition
    {
    }
}