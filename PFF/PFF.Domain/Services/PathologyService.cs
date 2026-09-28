using PFF.Domain.Commands;
using PFF.Domain.Errors;
using PFF.Domain.Model.Entities;
using PFF.Domain.Queries;
using PFF.Domain.Repositories;
using PFF.Tools.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace PFF.Domain.Services
{
    public class PathologyService : IPathologyRepository
    {
        private readonly ApplicationDbContext _dbContext;
        
        public PathologyService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Result<IEnumerable<Pathology>> Handle(GetPathologiesQuery query)
        {
            return Result<IEnumerable<Pathology>>.Success(_dbContext.Pathologies.AsEnumerable());
        }

        public Result Handle(CreatePathologyCommand command)
        {
            try 
            {
                Pathology pathology = new Pathology()
                {
                    Name = command.Name
                };
                _dbContext.Add(pathology);
                _dbContext.SaveChanges();
                return Result.Success();
            }

            catch (Exception)
            {
                return PathologyErrors.PathologyException;
            }
        }
    }
}
