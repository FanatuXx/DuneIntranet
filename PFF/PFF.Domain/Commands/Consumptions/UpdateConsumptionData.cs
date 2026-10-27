using PFF.Tools.CommandQuerySeparation;
using System;
using System.Collections.Generic;
using System.Text;

namespace PFF.Domain.Commands.Consumptions
{
    public record UpdateConsumptionData(
        int Id,
        string? DrugType,
        string? Frequency) : ICommandDefinition
    {
    }
}
