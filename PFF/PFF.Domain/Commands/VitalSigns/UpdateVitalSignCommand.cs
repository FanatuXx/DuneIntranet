using PFF.Tools.CommandQuerySeparation;
using System;
using System.Collections.Generic;
using System.Text;

namespace PFF.Domain.Commands.VitalSigns
{
    public record UpdateVitalSignCommand (
        int Id,
        int? HeartRate,
        string? BloodPressure,
        decimal? Temperature,
        int? OxygenSaturation,
        int? Height,
        float? Weight,
        float? BloodSugar,
        DateTime? Date,
        int? ConsultationId) : ICommandDefinition
    {
    }
}
