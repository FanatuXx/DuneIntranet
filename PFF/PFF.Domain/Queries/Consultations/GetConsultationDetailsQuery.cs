using PFF.Domain.Model.ResponseDtos;
using PFF.Tools.CommandQuerySeparation;
using System;
using System.Collections.Generic;
using System.Text;

namespace PFF.Domain.Queries.Consultations
{
    public record GetConsultationDetailsQuery(int Id) : IQueryDefinition<ConsultationDetailsDto>
    {
    }
}
