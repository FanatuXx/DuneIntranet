using Microsoft.EntityFrameworkCore;
using PFF.Domain.Commands.Medicines;
using PFF.Domain.Errors;
using PFF.Domain.Model.Entities;
using PFF.Domain.Queries.Medicines;
using PFF.Domain.Repositories;
using PFF.Tools.Results;
using System;
using System.Collections.Generic;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace PFF.Domain.Services
{
    public class MedicineService : IMedicineRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public MedicineService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<IEnumerable<Medicine>>> HandleAsync(GetMedicinesQuery query)
        {
            List<Medicine> medicines = await _dbContext.Medicines.ToListAsync();
            return Result<IEnumerable<Medicine>>.Success(medicines);
        }

        public async Task<Result<Medicine>> HandleAsync(GetMedicineDetailsQuery query)
        {
            Medicine? medicine = await _dbContext.Medicines
                .Include(m => m.ChronicTreatments)
                .Include(m => m.Prescriptions)
                .SingleOrDefaultAsync(m => m.Id == query.Id);

            if (medicine is null)
                return MedicineErrors.MedicineNotFound;

            return Result<Medicine>.Success(medicine);
        }

        public async Task<Result> HandleAsync(CreateMedicineCommand command, CancellationToken cancellationToken)
        {
            try
            {
                Medicine medicine = new Medicine()
                {
                    Name = command.Name,
                    HowToTake = command.HowToTake
                };

                await _dbContext.Medicines.AddAsync(medicine);
                await _dbContext.SaveChangesAsync();
                return Result.Success();
            }

            catch(Exception ex)
            {
                return MedicineErrors.MedicineException;
            }  
        }

        public async Task<Result> HandleAsync(UpdateMedicineCommand command, CancellationToken cancellationToken)
        {
            Medicine? medicine = await _dbContext.Medicines.FindAsync(command.Id);

            if (medicine is null)
                return MedicineErrors.MedicineNotFound;

            if (!string.IsNullOrWhiteSpace(command.Name))
                medicine.Name = command.Name;

            if (!string.IsNullOrWhiteSpace (command.HowToTake))
                medicine.HowToTake = command.HowToTake;

            await _dbContext.SaveChangesAsync();
            return Result.Success();
        }

        public async Task<Result> HandleAsync(DeleteMedicineCommand command, CancellationToken cancellationToken)
        {
            Medicine? medicine = await _dbContext.Medicines.FindAsync(command.Id);

            if (medicine is null)
                return MedicineErrors.MedicineNotFound;

            _dbContext.Remove(medicine);
            await _dbContext.SaveChangesAsync();
            return Result.Success();
        }
    }
}
