using Microsoft.EntityFrameworkCore;
using PFF.Domain.Commands.Pathology;
using PFF.Domain.Errors;
using PFF.Domain.Model.Entities;
using PFF.Domain.Queries.Pathologies;
using PFF.Domain.Repositories;
using PFF.Tools.Results;

namespace PFF.Domain.Services
{
    public class PathologyService : IPathologyRepository
    {
        private readonly ApplicationDbContext _dbContext;
        
        public PathologyService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<IEnumerable<Pathology>>> HandleAsync(GetPathologiesQuery query)
        {
            var pathologies = await _dbContext.Pathologies.ToListAsync();
            return Result<IEnumerable<Pathology>>.Success(pathologies);
        }

        public async Task<Result<Pathology>> HandleAsync(GetPathologyDetailsQuery query)
        {
            Pathology? pathology = await _dbContext.Pathologies.FindAsync(query.Id);

            if (pathology is null)
                return PathologyErrors.PathologyNotFound;

            return Result<Pathology>.Success(pathology);
        }

        public async Task<Result> HandleAsync(CreatePathologyCommand command, CancellationToken cancellationToken)
        {
            try 
            {
                Pathology pathology = new Pathology()
                {
                    Name = command.Name
                };

                await _dbContext.AddAsync(pathology);
                await _dbContext.SaveChangesAsync();
                return Result.Success();
            }

            catch (Exception)
            {
                return PathologyErrors.PathologyException;
            }
        }

        public async Task<Result> HandleAsync(UpdateChronicTreatmentCommand command, CancellationToken cancellationToken)
        {
            Pathology? pathology = await _dbContext.Pathologies.FindAsync(command.Id);

            if (pathology is null)
                return PathologyErrors.PathologyNotFound;

            if (!string.IsNullOrWhiteSpace(command.Name))
                pathology.Name = command.Name;

            await _dbContext.SaveChangesAsync();
            return Result.Success();
        }

        public async Task<Result> HandleAsync(DeleteChronicTreatmentCommand command, CancellationToken cancellationToken)
        {
            Pathology? pathology = await _dbContext.Pathologies.FindAsync(command.Id);

            if (pathology is null)
                return PathologyErrors.PathologyNotFound;

            _dbContext.Pathologies.Remove(pathology);
            return Result.Success();

        }
    }
}
