using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PFF.Domain.Commands.ChronicTreatment;
using PFF.Domain.Errors;
using PFF.Domain.Model.Entities;
using PFF.Domain.Queries.ChronicTreatments;
using PFF.Domain.Repositories;
using PFF.Tools.Results;
using System;
using System.Collections.Generic;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace PFF.Domain.Services
{
    public class ChronicTreatmentService : IChronicTreatmentRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ChronicTreatmentService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<IEnumerable<ChronicTreatment>>> HandleAsync(GetChronicTreatmentsQuery query)
        {
            List<ChronicTreatment> treatments = await _dbContext.ChronicTreatments.ToListAsync();

            return Result<IEnumerable<ChronicTreatment>>.Success(treatments); 
        }

        public async Task<Result<ChronicTreatment>> HandleAsync(GetChronicTreatmentDetailsQuery query)
        {
            ChronicTreatment? treatment = await _dbContext.ChronicTreatments
                .Include(t => t.Patient)
                .Include(t => t.Pathology)
                .Include(t => t.Medicines)
                .SingleOrDefaultAsync(t => t.Id == query.Id);

            if (treatment is null)
                return ChronicTreatmentErrors.ChronicTreatmentNotFound;

            return Result<ChronicTreatment>.Success(treatment);
        }

        public async Task<Result> HandleAsync(CreateChronicTreatmentCommand command, CancellationToken cancellationToken)
        {
            Patient? patient = await _dbContext.Patients.SingleOrDefaultAsync(p => p.Id == command.PatientId);

            if (patient is null)
                return PatientErrors.PatientNotFound;

            Pathology? pathology = await _dbContext.Pathologies.SingleOrDefaultAsync(patho => patho.Id == command.PathologyId);

            if (pathology is null)
                return PathologyErrors.PathologyNotFound;

            List<Medicine> medicines = await _dbContext.Medicines
                .Where(m => command.MedicineIds.Contains(m.Id))
                .ToListAsync();

            if (medicines.Count == 0)
                return MedicineErrors.MedicineNotFound;

            try
            {
                ChronicTreatment treatment = new ChronicTreatment()
                {
                    Dosage = command.Dosage,
                    PatientId = command.PatientId,
                    PathologyId = command.PathologyId,
                    Patient = patient,
                    Pathology = pathology,
                    Medicines = medicines
                };

                await _dbContext.AddAsync(treatment);
                await _dbContext.SaveChangesAsync();
                return Result.Success();

            }
            catch (Exception)
            {

                return ChronicTreatmentErrors.ChronicTreatmentException;
            }
        }

        public async Task<Result> HandleAsync(UpdateChronicTreatmentCommand command, CancellationToken cancellationToken)
        {
            ChronicTreatment? treatment = await _dbContext.ChronicTreatments.SingleOrDefaultAsync(t => t.Id == command.Id);

            if (treatment is null)
                return ChronicTreatmentErrors.ChronicTreatmentNotFound;

            if (!string.IsNullOrWhiteSpace(command.Dosage))
                treatment.Dosage = command.Dosage;

            if (command.PathologyId > 0)
            {   
                Pathology? newPathology = await _dbContext.Pathologies.SingleOrDefaultAsync(patho => patho.Id == command.PathologyId, CancellationToken.None);

                if (newPathology is null)
                    return PathologyErrors.PathologyNotFound;

                treatment.PathologyId = (int)command.PathologyId;
                treatment.Pathology = newPathology;
            }

            if (command.PatientId > 0)
            {
                Patient? newPatient = await _dbContext.Patients.SingleOrDefaultAsync(p => p.Id == command.PatientId, CancellationToken.None);

                if (newPatient is null)
                    return PatientErrors.PatientNotFound;

                treatment.PatientId = (int)command.PatientId;
                treatment.Patient = newPatient;
            }

            if (command.MedicineIds?.Count() > 0)
            {
                List<Medicine> medicines = await _dbContext.Medicines
                    .Where(m => command.MedicineIds.Contains(m.Id))
                    .ToListAsync();

                if (medicines.Count == 0)
                    return MedicineErrors.MedicineNotFound;

                treatment.Medicines = medicines;
            }

            await _dbContext.SaveChangesAsync();
            return Result.Success();
        }

        public async Task<Result> HandleAsync(DeleteChronicTreatmentCommand command, CancellationToken cancellationToken)
        {
            ChronicTreatment? treatment = await _dbContext.ChronicTreatments.SingleOrDefaultAsync(t => t.Id == command.Id);

            if (treatment is null)
                return ChronicTreatmentErrors.ChronicTreatmentNotFound;

            _dbContext.Remove(treatment);
            await _dbContext.SaveChangesAsync();
            return Result.Success();
        }
    }
}
