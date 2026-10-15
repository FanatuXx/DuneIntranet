using System;
using System.Collections.Generic;
using System.Text;
using PFF.Tools.CommandQuerySeparation;

namespace PFF.Domain.Commands.Pathology
{
    public record DeleteChronicTreatmentCommand (int Id) : ICommandDefinition
    {
    }
}
