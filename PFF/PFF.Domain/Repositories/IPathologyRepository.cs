using PFF.Domain.Commands.Pathology;
using PFF.Domain.Model.Entities;
using PFF.Domain.Queries.Pathologies;
using PFF.Tools.CommandQuerySeparation;

namespace PFF.Domain.Repositories
{
    public interface IPathologyRepository :
        ICommandAsyncHandler<CreatePathologyCommand>,
        ICommandAsyncHandler<UpdateChronicTreatmentCommand>,
        ICommandAsyncHandler<DeleteChronicTreatmentCommand>,
        IQueryAsyncHandler<GetPathologiesQuery, IEnumerable<Pathology>>,
        IQueryAsyncHandler<GetPathologyDetailsQuery, Pathology>
    {
    }
}
