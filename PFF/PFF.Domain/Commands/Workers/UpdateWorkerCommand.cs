using PFF.Tools.CommandQuerySeparation;
using System;
using System.Collections.Generic;
using System.Text;

namespace PFF.Domain.Commands.Workers
{
    public record UpdateWorkerCommand(
        int SSIN,
        string? FirstName,
        string? LastName,
        string? Gender,
        DateTime? BirthDate,
        string? Email,
        string? PhoneNumber,
        string? Occupation,
        string? Street,
        string? Number,
        int? ZipCode,
        string? Town,
        string? Country) : ICommandDefinition
    {
    }
}
