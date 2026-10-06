using PFF.Tools.CommandQuerySeparation;
using System;
using System.Collections.Generic;
using System.Text;

namespace PFF.Domain.Commands.PatientAddress
{
    public record CreatePatientAddressCommand(
        int PatientId,
        string? Street,
        string? Number,
        int ZipCode,
        string? Town,
        string? Country) : ICommandDefinition
    {
    }
}
