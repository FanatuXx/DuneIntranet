using PFF.Domain.Model.Entities;
using PFF.Tools.CommandQuerySeparation;
using System;
using System.Collections.Generic;
using System.Text;

namespace PFF.Domain.Queries.Consultations
{
    public record GetConsultationsQuery : IQueryDefinition<IEnumerable<Consultation>>
    {
    }
}
