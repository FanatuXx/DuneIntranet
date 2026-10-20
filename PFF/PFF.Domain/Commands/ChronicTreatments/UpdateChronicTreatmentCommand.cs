using PFF.Tools.CommandQuerySeparation;
using System;
using System.Collections.Generic;
using System.Text;

namespace PFF.Domain.Commands.ChronicTreatment
{
    public record UpdateChronicTreatmentCommand(
        int Id,
        string? Dosage,
        int? PatientId,
        int? PathologyId,
        IList<int>? MedicineIds) : ICommandDefinition
    {
    }
}
