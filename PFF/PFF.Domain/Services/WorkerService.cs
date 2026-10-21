using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
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
            Worker? worker = await _dbContext.Workers.FindAsync(query.SSIN);

            if (worker is null)
                return WorkerErrors.WorkerNotFound;

            return Result<Worker>.Success(worker);
        }

        public async Task<Result> HandleAsync(CreateWorkerCommand command, CancellationToken cancellationToken)
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

        public async Task<Result> HandleAsync(UpdateWorkerCommand command, CancellationToken cancellationToken)
        {
            Worker? worker = await _dbContext.Workers.FindAsync(command.Id);

            if (worker is null)
                return WorkerErrors.WorkerNotFound;

            if (!string.IsNullOrWhiteSpace(command.FirstName))
                worker.FirstName = command.FirstName;

            if (!string.IsNullOrWhiteSpace(command.LastName))
                worker.LastName = command.LastName;

            if (command.Gender is not null)
                worker.Gender = command.Gender;

            if (command.BirthDate is not null)
                worker.BirthDate = Convert.ToDateTime(command.BirthDate);

            if (!string.IsNullOrWhiteSpace(command.Email))
                worker.Email = command.Email;

            if (!string.IsNullOrWhiteSpace(command.PhoneNumber))
                worker.PhoneNumber = command.PhoneNumber;

            if (!string.IsNullOrWhiteSpace(command.Occupation))
                worker.Occupation = command.Occupation;

            if (!string.IsNullOrWhiteSpace(command.Street))
                worker.Street = command.Street;

            if (!string.IsNullOrWhiteSpace(command.Number))
                worker.Number = command.Number;

            if (!string.IsNullOrWhiteSpace(command.ZipCode))
                worker.ZipCode = command.ZipCode;

            if (!string.IsNullOrWhiteSpace(command.Town))
                worker.Town = command.Town;

            if (!string.IsNullOrWhiteSpace(command.Country))
                worker.Country = command.Country;

            await _dbContext.SaveChangesAsync();
            return Result.Success();
        }

        public async Task<Result> HandleAsync(DeleteWorkerCommand command, CancellationToken cancellationToken)
        {
            Worker? worker = await _dbContext.Workers.FindAsync(command.Id);

            if (worker is null)
                return WorkerErrors.WorkerNotFound;

            _dbContext.Workers.Remove(worker);
            await _dbContext.SaveChangesAsync();
            return Result.Success();
        }
    }
}