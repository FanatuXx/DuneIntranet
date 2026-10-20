using PFF.Tools.CommandQuerySeparation;
using System;
using System.Collections.Generic;
using System.Text;

namespace PFF.Domain.Commands.Consultations
{
    public record DeleteConsultationCommand(int Id) : ICommandDefinition
    {
    }
}
