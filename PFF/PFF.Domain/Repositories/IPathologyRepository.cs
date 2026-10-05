using PFF.Domain.Commands.Pathology;
using PFF.Domain.Model.Entities;
using PFF.Domain.Queries;
using PFF.Tools.CommandQuerySeparation;
using System;
using System.Collections.Generic;
using System.Text;

namespace PFF.Domain.Repositories
{
    public interface IPathologyRepository :
        ICommandHandler<CreatePathologyCommand>,
        IQueryHandler<GetPathologiesQuery, IEnumerable<Pathology>>
    {
    }
}
