using System;
using System.Collections.Generic;
using System.Text;
using PFF.Tools.CommandQuerySeparation;

namespace PFF.Domain.Commands.ChronicTreatment
{
    public record DeleteChronicTreatmentCommand(int Id) : ICommandDefinition
    {
    }
}
