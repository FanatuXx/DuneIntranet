using PFF.Domain.Model.Entities;
using PFF.Domain.Model.ResponseDtos;
using PFF.Tools.CommandQuerySeparation;
using System;
using System.Collections.Generic;
using System.Text;

namespace PFF.Domain.Queries.Patients
{
    public record GetPatientDetailsQuery(int Id) : IQueryDefinition<PatientDetailsDto>
    {
    }
}
