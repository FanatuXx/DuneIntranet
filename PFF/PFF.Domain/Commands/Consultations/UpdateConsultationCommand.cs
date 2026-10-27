using PFF.Domain.Commands.Prescriptions;
using PFF.Domain.Commands.VitalSigns;
using PFF.Domain.Model.Entities;
using PFF.Tools.CommandQuerySeparation;

namespace PFF.Domain.Commands.Consultations
{
    public record UpdateConsultationCommand(
        int Id,
        string? Motive,
        string? Subjective,
        string? Objective,
        string? Assessment,
        string? Plan,
        string? Description,
        DateTime? Date,
        int? PatientId,

        IList<int>? WorkersIds,
        IList<int>? PathologiesIds,

        IList<UpdatePrescriptionData>? PrescriptionsToUpdate,
        IList<CreatePrescriptionData>? PrescriptionsToCreate,

        IList<UpdateVitalSignData>? VitalSignsToUpdate,
        IList<CreateVitalSignData>? VitalSignsToCreate) : ICommandDefinition
    {
    }
}