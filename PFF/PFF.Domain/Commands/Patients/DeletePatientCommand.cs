using PFF.Tools.CommandQuerySeparation;
using System;
using System.Collections.Generic;
using System.Text;

namespace PFF.Domain.Commands.Patient
{
    public record DeletePatientCommand(int Id) : ICommandDefinition
    {
    }
}
