using PFF.Tools.CommandQuerySeparation;
using System;
using System.Collections.Generic;
using System.Text;

namespace PFF.Domain.Commands
{
    public record CreatePathologyCommand(string Name) : ICommandDefinition;
}
