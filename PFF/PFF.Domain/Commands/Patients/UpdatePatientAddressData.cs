using PFF.Tools.CommandQuerySeparation;
using System;
using System.Collections.Generic;
using System.Text;

namespace PFF.Domain.Commands.Patients
{
    public record UpdatePatientAddressData(
        int Id,
        string? Street,
        string? Number,
        string? ZipCode,
        string? Town,
        string? Country) : ICommandDefinition
    {
    }
}
