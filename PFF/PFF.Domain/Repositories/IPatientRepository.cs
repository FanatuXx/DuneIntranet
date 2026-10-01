using PFF.Domain.Commands;
using PFF.Domain.Model.Entities;
using PFF.Domain.Queries;
using PFF.Tools.CommandQuerySeparation;
using System;
using System.Collections.Generic;
using System.Text;

namespace PFF.Domain.Repositories
{
    public interface IPatientRepository :
        ICommandHandler<CreatePatientCommand>,
        ICommandHandler<UpdatePatientCommand>,
        IQueryHandler<GetPatientsQuery, IEnumerable<Patient>>,
        IQueryHandler<GetPatientDetailsQuery, Patient>
    {
    }
}
