using PFF.Domain.Commands.Prescriptions;
using PFF.Domain.Commands.VitalSigns;
using PFF.Domain.Model.Entities;
using PFF.Tools.CommandQuerySeparation;
using System;
using System.Collections.Generic;
using System.Text;

namespace PFF.Domain.Commands.Consultations
{
    public record CreateConsultationCommand(
        string Motive,
        string? Subjective,
        string? Objective,
        string? Assessment,
        string? Plan,
        string? Description,
        DateTime Date,
        int PatientId,

        IList<int> WorkersIds,
        IList<int> PathologiesIds,

        IList<CreatePrescriptionData>? Prescriptions,
        IList<CreateVitalSignData>? VitalSigns) : ICommandDefinition
    {
    }
}