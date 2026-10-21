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
                    Date = DateTime.Now,
                    PatientId = command.PatientId,
                    Patient = patient,
                    Workers = command.Workers,
                    Prescriptions = command.Prescriptions,
                    VitalSigns = command.VitalSigns,
                    Pathologies = command.Pathologies
                };

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

            Patient? patient = await _dbContext.Patients.FindAsync(command.PatientId);

            if(patient is null)
                return PatientErrors.PatientNotFound;

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
                consultation.PatientId = (int)command.PatientId;
                consultation.Patient = patient;
            }

            if (command.Workers is not null && command.Workers.Count > 0)
                consultation.Workers = command.Workers;

            if (command.Prescriptions is not null && command.Prescriptions.Count > 0)
                consultation.Prescriptions = command.Prescriptions;

            if (command.VitalSigns is not null && command.VitalSigns.Count > 0)
                consultation.VitalSigns = command.VitalSigns;

            if (command.Pathologies is not null && command.Pathologies.Count > 0)
                consultation.Pathologies = command.Pathologies;

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