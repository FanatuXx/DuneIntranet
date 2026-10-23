using PFF.Tools.CommandQuerySeparation;
using System;
using System.Collections.Generic;
using System.Text;

namespace PFF.Domain.Commands.VitalSigns
{
    public record CreateVitalSignData(
        int HeartRate,
        string? BloodPressure,
        decimal? Temperature,
        int OxygenSaturation,
        int? Height,
        float? Weight,
        float? BloodSugar,
        DateTime Date) : ICommandDefinition
    {
    }
}
