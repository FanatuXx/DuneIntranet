using PFF.Tools.CommandQuerySeparation;
using System;
using System.Collections.Generic;
using System.Text;

namespace PFF.Domain.Commands.Medicines
{
    public record CreateMedicineCommand(string Name, string HowToTake) : ICommandDefinition
    {
    }
}
