using PFF.Tools.CommandQuerySeparation;
using System;
using System.Collections.Generic;
using System.Text;

namespace PFF.Domain.Commands.Workers
{
    public record DeleteWorkerCommand(int SSIN) : ICommandDefinition
    {
    }
}
