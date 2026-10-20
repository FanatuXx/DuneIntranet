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
        IList<Worker>? Workers,
        IList<Pathology>? Pathologies,
        IList<VitalSign>? VitalSigns,
        IList<Prescription>? Prescriptions) : ICommandDefinition
    {
    }
}