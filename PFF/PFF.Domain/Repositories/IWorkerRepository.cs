using PFF.Domain.Commands.Workers;
using PFF.Domain.Model.Entities;
using PFF.Domain.Queries.Workers;
using PFF.Tools.CommandQuerySeparation;
using System;
using System.Collections.Generic;
using System.Text;

namespace PFF.Domain.Repositories
{
    public interface IWorkerRepository :
        IQueryAsyncHandler<GetWorkersQuery, IEnumerable<Worker>>,
        IQueryAsyncHandler<GetWorkerDetailsQuery, Worker>,
        IQueryAsyncHandler<GetSearchedWorkersQuery, IEnumerable<Worker>>,
        ICommandAsyncHandler<CreateWorkerCommand>,
        ICommandAsyncHandler<UpdateWorkerCommand>,
        ICommandAsyncHandler<DeleteWorkerCommand>
    {
    }
}
