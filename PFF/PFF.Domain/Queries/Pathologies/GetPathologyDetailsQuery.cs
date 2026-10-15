using PFF.Domain.Model.Entities;
using PFF.Tools.CommandQuerySeparation;

namespace PFF.Domain.Queries.Pathologies
{
    public record GetPathologyDetailsQuery(int Id) : IQueryDefinition<Pathology>
    {
    }
}
