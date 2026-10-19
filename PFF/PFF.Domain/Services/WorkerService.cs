using Microsoft.EntityFrameworkCore;
using PFF.Domain.Commands.Workers;
using PFF.Domain.Errors;
using PFF.Domain.Model.Entities;
using PFF.Domain.Queries.Workers;
using PFF.Domain.Repositories;
using PFF.Tools.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace PFF.Domain.Services
{
    public class WorkerService : IWorkerRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public WorkerService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<IEnumerable<Worker>>> HandleAsync(GetWorkersQuery query)
        {
            IList<Worker> workers = await _dbContext.Workers.ToListAsync();
            return Result<IEnumerable<Worker>>.Success(workers);
        }

        public async Task<Result<Worker>> HandleAsync(GetWorkerDetailsQuery query)
        {
            Worker? worker = await _dbContext.Workers.FindAsync(query.Id);

            if (worker is null)
                return WorkerErrors.WorkerNotFound;

            return Result<Worker>.Success(worker);
        }

        public async Task<Result> HandleAsync(CreateWorkerCommand command)
        {
            try
            {
                Worker worker = new Worker()
                {
                    SSIN = command.SSIN,
                    FirstName = command.FirstName,
                    LastName = command.LastName,
                    Gender = command.Gender,
                    BirthDate = command.BirthDate,
                    Email = command.Email,
                    PhoneNumber = command.PhoneNumber,
                    Occupation = command.Occupation,
                    Street = command.Street,
                    Number = command.Number,
                    ZipCode = command.ZipCode,
                    Town = command.Town,
                    Country = command.Country
                };

                await _dbContext.Workers.AddAsync(worker);
                await _dbContext.SaveChangesAsync();
                return Result.Success();
            }

            catch (Exception ex)
            {
                return WorkerErrors.WorkerException;
            }
        }

        public async Task<Result> HandleAsync(UpdateWorkerCommand command)
        {
            Worker? worker = await _dbContext.Workers.FindAsync(command.Id);

            if (worker is null)
                return WorkerErrors.WorkerNotFound;

            if(!string.IsNullOrWhiteSpace(command.SSIN))

        }
    }
}