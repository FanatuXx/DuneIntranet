using PFF.Domain.Model.Entities;
using PFF.Tools.CommandQuerySeparation;
using System;
using System.Collections.Generic;
using System.Text;

namespace PFF.Domain.Commands.Prescriptions
{
    public record CreatePrescriptionCommand(
        DateTime StartDate,
        DateTime EndDate,
        string TakeFrequency,
        int ConsultationId,
        int PatientId,
        int WorkerId,
        IList<Medicine> Medicines) : ICommandDefinition
    {
    }
}