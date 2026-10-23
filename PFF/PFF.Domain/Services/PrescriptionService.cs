using Microsoft.EntityFrameworkCore;
using PFF.Domain.Commands.Prescriptions;
using PFF.Domain.Errors;
using PFF.Domain.Model.Entities;
using PFF.Domain.Queries.Prescriptions;
using PFF.Domain.Repositories;
using PFF.Tools.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace PFF.Domain.Services
{
    public class PrescriptionService : IPrescriptionRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public PrescriptionService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<IEnumerable<Prescription>>> HandleAsync(GetPrescriptionsQuery query)
        {
            IList<Prescription> prescriptions = await _dbContext.Prescriptions.ToListAsync();
            return Result<IEnumerable<Prescription>>.Success(prescriptions);
        }

        public async Task<Result<Prescription>> HandleAsync(GetPrescriptionDetailsQuery query)
        {
            Prescription? prescription = await _dbContext.Prescriptions
                .Include(pre => pre.Consultation)
                .Include(pre => pre.Patient)
                .Include(pre => pre.Medicines)
                .SingleOrDefaultAsync(pre => pre.Id == query.Id);

            if (prescription is null)
                return PrescriptionErrors.PrescriptionNotFound;

            return Result<Prescription>.Success(prescription);
        }

        public async Task<Result> HandleAsync(CreatePrescriptionCommand command, CancellationToken cancellationToken)
        {
            IList<Medicine> medicines = await _dbContext.Medicines
                .Where(m => command.MedicinesIds.Contains(m.Id))
                .ToListAsync(cancellationToken);

            if (medicines.Count != command.MedicinesIds.Count)
            {
                return MedicineErrors.MedicineNotFound;
            }

            try
            {
                Prescription prescription = new Prescription()
                {
                    StartDate = command.StartDate,
                    EndDate = command.EndDate,
                    TakeFrequency = command.TakeFrequency,
                    ConsultationId = command.ConsultationId,
                    PatientId = command.PatientId,
                    WorkerId = command.WorkerId,
                    Medicines = medicines
                };

                await _dbContext.Prescriptions.AddAsync(prescription);
                await _dbContext.SaveChangesAsync();
                return Result.Success();
            }

            catch (Exception ex)
            {
                return PrescriptionErrors.PrescriptionException;
            }
        }

        public async Task<Result> HandleAsync(UpdatePrescriptionCommand command, CancellationToken cancellationToken)
        {
            Prescription? prescription = await _dbContext.Prescriptions.FindAsync(command.Id);

            if (prescription is null)
                return PrescriptionErrors.PrescriptionNotFound;

            if (command.StartDate is not null)
                prescription.StartDate = command.StartDate.Value;

            if (command.EndDate is not null)
                prescription.EndDate = command.EndDate.Value;

            if (!string.IsNullOrWhiteSpace(command.TakeFrequency))
                prescription.TakeFrequency = command.TakeFrequency;

            if (command.ConsultationId > 0)
                prescription.ConsultationId = (int)command.ConsultationId;

            if (command.PatientId > 0)
                prescription.PatientId = (int)command.PatientId;

            if (command.WorkerId > 0)
                prescription.WorkerId = (int)command.WorkerId;

            if (command.Medicines is not null && command.Medicines.Count > 0)
                prescription.Medicines = command.Medicines;

            await _dbContext.SaveChangesAsync();
            return Result.Success();
        }

        public async Task<Result> HandleAsync(DeletePrescriptionCommand command, CancellationToken cancellationToken)
        {
            Prescription? prescription = await _dbContext.Prescriptions.FindAsync(command.Id);

            if (prescription is null)
                return PrescriptionErrors.PrescriptionNotFound;

            _dbContext.Prescriptions.Remove(prescription);
            await _dbContext.SaveChangesAsync();
            return Result.Success();
        }
    }
}

