using PFF.Tools.CommandQuerySeparation;
using System;
using System.Collections.Generic;
using System.Text;

namespace PFF.Domain.Commands.ChronicTreatment
{
    public record CreateChronicTreatmentCommand(
        string Dosage,
        int PathologyId,
        int PatientId) : ICommandDefinition;
}
