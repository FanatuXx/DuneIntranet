using PFF.Tools.CommandQuerySeparation;
using System;
using System.Collections.Generic;
using System.Text;

namespace PFF.Domain.Commands.Prescriptions
{
    public record CreatePrescriptionData(
        DateTime StartDate,
        DateTime EndDate,
        string TakeFrequency,
        int PrescriberId,
        IList<int> MedicinesIds) : ICommandDefinition
    {
    }
}
