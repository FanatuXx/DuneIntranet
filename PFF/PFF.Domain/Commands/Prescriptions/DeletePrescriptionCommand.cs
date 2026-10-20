using PFF.Tools.CommandQuerySeparation;
using System;
using System.Collections.Generic;
using System.Text;

namespace PFF.Domain.Commands.Prescriptions
{
    public record DeletePrescriptionCommand(int Id) : ICommandDefinition
    {
    }
}
