using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using PFF.Domain.Commands.Consultations;
using PFF.Domain.Commands.Prescriptions;
using PFF.Domain.Commands.VitalSigns;
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

            IList<int> workersIds = command.WorkersIds.Distinct().ToList();         //Disctinct() s'assure qu'on ne compte pas 2x le même Id dans la liste
            IList<int> pathologiesIds = command.PathologiesIds.Distinct().ToList();

            IList<Worker> workers = await _dbContext.Workers
                .Where(w => command.WorkersIds.Contains(w.Id))
                .ToListAsync(cancellationToken);

            IList<Pathology> pathologies = await _dbContext.Pathologies
                .Where(p => command.PathologiesIds.Contains(p.Id))
                .ToListAsync();

            if (workers.Count != workersIds.Distinct().Count()) 
            {
                return WorkerErrors.WorkerNotFound;
            }

            if (pathologies.Count != pathologiesIds.Distinct().Count())
            {
                return PathologyErrors.PathologyNotFound;
            }

            try
            {
                Consultation consultation = new Consultation() // Crée la consultation en renseignant le patients, les travailleurs et les pathologies relatives à la consult (qui existent déjà dans la DB)
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

                foreach (CreateVitalSignData vitalData in command.VitalSigns) //Boucle dans la liste de prise de paramètres et ajoute chacune d'elle dans la consultation
                {
                    VitalSign vitalSign = new VitalSign
                    {
                        HeartRate = vitalData.HeartRate,
                        BloodPressure = vitalData.BloodPressure,
                        Temperature = vitalData.Temperature,
                        OxygenSaturation = vitalData.OxygenSaturation,
                        Height = vitalData.Height,
                        Weight = vitalData.Weight,
                        BloodSugar = vitalData.BloodSugar,
                        Date = command.Date,

                        Consultation = consultation
                    };

                    consultation.VitalSigns!.Add(vitalSign);
                }

                foreach (CreatePrescriptionData prescriptionData in command.Prescriptions) //Boucle dans la liste de prescriptions et ajoute chacune d'elle dans la consultation
                {
                    IList<int> medicineIds = prescriptionData.MedicinesIds
                        .Distinct()
                        .ToList();

                    IList<Medicine> medicines = await _dbContext.Medicines
                        .Where(m => medicineIds.Contains(m.Id))
                        .ToListAsync(cancellationToken);

                    if (medicines.Count != medicineIds.Count)
                    {
                        return MedicineErrors.MedicineNotFound;
                    }

                    Worker? prescriber = await _dbContext.Workers.FindAsync(prescriptionData.PrescriberId);

                    if (prescriber is null)
                        return WorkerErrors.WorkerNotFound;

                    Prescription prescription = new Prescription
                    {
                        StartDate = prescriptionData.StartDate,
                        EndDate = prescriptionData.EndDate,
                        TakeFrequency = prescriptionData.TakeFrequency,

                        Worker = prescriber,
                        Patient = patient,
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
            Consultation? consultation = await _dbContext.Consultations
                .Include(c => c.Patient)
                .Include(c => c.Workers)
                .Include(c => c.Pathologies)
                .Include(c => c.VitalSigns)
                .Include(c => c.Prescriptions)
                .SingleOrDefaultAsync(c => c.Id == command.Id
                );
                

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
                consultation.Date = command.Date.Value;


            if (command.PatientId > 0)
            {
                Patient? patient = await _dbContext.Patients.FindAsync(command.PatientId);

                if (patient is null)
                    return PatientErrors.PatientNotFound;

                consultation.Patient = patient;
            }

            if (command.WorkersIds?.Count > 0)
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

            if (command.PathologiesIds?.Count > 0)
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


            IList<VitalSign> existingVitalSigns = consultation.VitalSigns!;

            foreach (UpdateVitalSignData data in command.VitalSignsToUpdate ?? new List<UpdateVitalSignData>())
            {
                VitalSign? vitalSign = existingVitalSigns?.SingleOrDefault(vs => vs.Id == data.Id);

                if (vitalSign is null)
                    return VitalSignErrors.VitalSignNotFound;

                vitalSign.HeartRate = data.HeartRate ?? vitalSign.HeartRate;                                    // ?? permet d'attribuer la valeur uniquement si elle n'est pas null (la valeur qui précède le ??), sinon on prend la valeur APRES les "??"
                vitalSign.BloodPressure = data.BloodPressure ?? vitalSign.BloodPressure;
                vitalSign.Temperature = data.Temperature ?? vitalSign.Temperature;
                vitalSign.OxygenSaturation = data.OxygenSaturation ?? vitalSign.OxygenSaturation;
                vitalSign.Height = data.Height ?? vitalSign.Height;
                vitalSign.Weight = data.Weight ?? vitalSign.Weight;
                vitalSign.BloodSugar = data.BloodSugar ?? vitalSign.BloodSugar;
                vitalSign.Date = data.Date ?? vitalSign.Date;
            }

            foreach (CreateVitalSignData data in command.VitalSignsToCreate ?? new List<CreateVitalSignData>())
            {
                VitalSign vitalSign = new VitalSign
                {
                    HeartRate = data.HeartRate,
                    BloodPressure = data.BloodPressure,
                    Temperature = data.Temperature,
                    OxygenSaturation = data.OxygenSaturation,
                    Height = data.Height,
                    Weight = data.Weight,
                    BloodSugar = data.BloodSugar,
                    Date = command.Date,
                    Consultation = consultation
                };

                existingVitalSigns!.Add(vitalSign);
            }

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