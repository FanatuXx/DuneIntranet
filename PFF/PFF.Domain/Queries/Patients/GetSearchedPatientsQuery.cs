using PFF.Domain.Model.Entities;
using PFF.Tools.CommandQuerySeparation;
using System;
using System.Collections.Generic;
using System.Text;

namespace PFF.Domain.Queries.Patients
{
    public record GetSearchedPatientsQuery (string Input) : IQueryDefinition<IEnumerable<Patient>>
    {
    }
}
