using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using PFF.Domain.Commands.Consultations;
using PFF.Domain.Errors;
using PFF.Domain.Model.Entities;
using PFF.Domain.Queries.Consultations;
using PFF.Domain.Repositories;
using PFF.Tools.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace PFF.Domain.Services
{
    public class ConsultationService : IConsultationRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ConsultationService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<IEnumerable<Consultation>>> HandleAsync(GetConsultationsQuery query)
        {
            List<Consultation> consultations = await _dbContext.Consultations.ToListAsync();
            return Result<IEnumerable<Consultation>>.Success(consultations);
        }

        public async Task<Result<Consultation>> HandleAsync(GetConsultationDetailsQuery query)
        {
            Consultation? consultation = await _dbContext.Consultations
                .Include(c => c.Patient)
                .Include(c => c.Pathologies)
                .Include(c => c.VitalSigns)
                .Include(c => c.Prescriptions)
                .SingleOrDefaultAsync(c => c.Id == query.Id);

            if (consultation is null)
                return ConsultationErrors.ConsultationNotFound;

            return Result<Consultation>.Success(consultation);
        }

        public async Task<Result> HandleAsync(CreateConsultationCommand command, CancellationToken cancellationToken)
        {
            Patient? patient = await _dbContext.Patients.FindAsync(command.PatientId);
            
            if (patient is null)
                return PatientErrors.PatientNotFound;

            var workersIds = command.WorkersIds.Distinct().ToList();
            var pathologiesIds = command.PathologiesIds.Distinct().ToList();

            IList<Worker> workers = await _dbContext.Workers
                .Where(w => command.WorkersIds.Contains(w.Id))
                .ToListAsync(cancellationToken);

            IList<Pathology> pathologies = await _dbContext.Pathologies
                .Where(p => command.PathologiesIds.Contains(p.Id))
                .ToListAsync();

            if (workers.Count != workersIds.Distinct().Count()) //Disctinct() s'assure qu'on ne compte pas 2x le même Id dans la liste
            {
                return WorkerErrors.WorkerNotFound;
            }

            if (pathologies.Count != pathologiesIds.Distinct().Count())
            {
                return PathologyErrors.PathologyNotFound;
            }

            try
            {
                Consultation consultation = new Consultation()
                {
                    Motive = command.Motive,
                    Subjective = command.Subjective,
                    Objective = command.Objective,
                    Assessment = command.Assessment,
                    Plan = command.Plan,
                    Description = command.Description,
                    Date = command.Date,

                    // PatientId = command.PatientId -> La ligne du dessous permet d'automatiquement renseigner le patient Id grâce à la FK & Entity Framework
                    Patient = patient,             
                    Workers = workers,
                    Pathologies = pathologies
                };

                foreach (var vitalData in command.VitalSigns)
                {
                    consultation.VitalSigns!.Add(new VitalSign
                    {
                        HeartRate = vitalData.HeartRate,
                        BloodPressure = vitalData.BloodPressure,
                        Temperature = vitalData.Temperature,
                        OxygenSaturation = vitalData.OxygenSaturation,
                        Height = vitalData.Height,
                        Weight = vitalData.Weight,
                        BloodSugar = vitalData.BloodSugar,

                        Consultation = consultation
                    });
                }

                foreach (var prescriptionData in command.Prescriptions)
                {
                    var medicineIds = prescriptionData.MedicinesIds
                        .Distinct()
                        .ToList();

                    var medicines = await _dbContext.Medicines
                        .Where(m => medicineIds.Contains(m.Id))
                        .ToListAsync(cancellationToken);

                    if (medicines.Count != medicineIds.Count)
                    {
                        return MedicineErrors.MedicineNotFound;
                    }

                    var prescription = new Prescription
                    {
                        StartDate = prescriptionData.StartDate,
                        EndDate = prescriptionData.EndDate,
                        TakeFrequency = prescriptionData.TakeFrequency,

                        Consultation = consultation,
                        Medicines = medicines
                    };

                    consultation.Prescriptions!.Add(prescription);
                }

                await _dbContext.Consultations.AddAsync(consultation);
                await _dbContext.SaveChangesAsync();

                return Result.Success();
            }

            catch (Exception ex)
            {
                return ConsultationErrors.ConsultationException;
            }
        }

        public async Task<Result> HandleAsync(UpdateConsultationCommand command, CancellationToken cancellationToken)
        {
            Consultation? consultation = await _dbContext.Consultations.FindAsync(command.Id);

            if (consultation is null)
                return ConsultationErrors.ConsultationNotFound;

            if (!string.IsNullOrWhiteSpace(command.Motive))
                consultation.Motive = command.Motive;

            if (!string.IsNullOrWhiteSpace(command.Subjective))
                consultation.Subjective = command.Subjective;

            if (!string.IsNullOrWhiteSpace(command.Objective))
                consultation.Objective = command.Objective;

            if (!string.IsNullOrWhiteSpace(command.Assessment))
                consultation.Assessment = command.Assessment;

            if (!string.IsNullOrWhiteSpace(command.Plan))
                consultation.Plan = command.Plan;

            if (!string.IsNullOrWhiteSpace(command.Description))
                consultation.Description = command.Description;

            if (command.Date is not null)
                consultation.Date = Convert.ToDateTime(command.Date);


            if (command.PatientId > 0)
            {
                Patient? patient = await _dbContext.Patients.FindAsync(command.PatientId);

                if (patient is null)
                    return PatientErrors.PatientNotFound;

                consultation.Patient = patient;
            }


            if (command.WorkersIds is not null && command.WorkersIds.Count > 0)
            {
                IList<Worker>? workers = await _dbContext.Workers
                    .Where(w => command.WorkersIds.Contains(w.Id))
                    .ToListAsync();

                if (workers.Count != command.WorkersIds?.Distinct().Count())
                {
                    return WorkerErrors.WorkerNotFound;
                }

                consultation.Workers = workers;
            }
                

            if (command.PathologiesIds is not null && command.PathologiesIds.Count > 0)
            {
                IList<Pathology>? pathologies = await _dbContext.Pathologies
                    .Where(p => command.PathologiesIds.Contains(p.Id))
                    .ToListAsync();

                if (pathologies.Count != command.PathologiesIds.Distinct().Count())
                {
                    return PathologyErrors.PathologyNotFound;
                }

                consultation.Pathologies = pathologies;
            }


            //if (command.Prescriptions is not null && command.Prescriptions.Count > 0)
            //    consultation.Prescriptions = command.Prescriptions;

            //if (command.VitalSigns is not null && command.VitalSigns.Count > 0)
            //    consultation.VitalSigns = command.VitalSigns;




            await _dbContext.SaveChangesAsync();
            return Result.Success();
        }

        public async Task<Result> HandleAsync(DeleteConsultationCommand command, CancellationToken cancellationToken)
        {
            Consultation? consultation = await _dbContext.Consultations.FindAsync(command.Id);

            if (consultation is null)
                return ConsultationErrors.ConsultationNotFound;

            _dbContext.Consultations.Remove(consultation);
            await _dbContext.SaveChangesAsync();
            return Result.Success();
        }
    }
}