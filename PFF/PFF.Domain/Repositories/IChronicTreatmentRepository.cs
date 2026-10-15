using PFF.Domain.Commands.ChronicTreatment;
using PFF.Domain.Model.Entities;
using PFF.Domain.Queries.ChronicTreatments;
using PFF.Tools.CommandQuerySeparation;
using System;
using System.Collections.Generic;
using System.Text;

namespace PFF.Domain.Repositories
{
    public interface IChronicTreatmentRepository :
        IQueryAsyncHandler<GetChronicTreatmentsQuery, IEnumerable<ChronicTreatment>>,
        IQueryAsyncHandler<GetChronicTreatmentDetailsQuery, ChronicTreatment>,
        ICommandAsyncHandler<CreateChronicTreatmentCommand>,
        ICommandAsyncHandler<UpdateChronicTreatmentCommand>,
        ICommandAsyncHandler<DeleteChronicTreatmentCommand>
    {
    }
}
