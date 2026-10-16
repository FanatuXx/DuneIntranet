using PFF.Domain.Model.Entities;
using PFF.Tools.CommandQuerySeparation;

namespace PFF.Domain.Queries.ChronicTreatments
{
    public record GetChronicTreatmentDetailsQuery(int Id) : IQueryDefinition<ChronicTreatment>
    {
    }
}
