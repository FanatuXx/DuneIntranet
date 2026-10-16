using PFF.Domain.Model.Entities;
using PFF.Tools.CommandQuerySeparation;
using System;
using System.Collections.Generic;
using System.Text;

namespace PFF.Domain.Commands.ChronicTreatment
{
    public record CreateChronicTreatmentCommand(
        string Dosage,
        int PatientId,
        int PathologyId,
        List<int> MedicineIds) : ICommandDefinition;
}
