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
            try
            {
                Prescription prescription = new Prescription()
                {
                    StartDate = command.StartDate,
                    EndDate = command.EndDate,
                    TakeFrequency = command.TakeFrequency,
                    ConsultationId = command.ConsultationId,
                    PatientId = command.PatientId,
                    WorkerSSIN = command.WorkerSSIN,
                    Medicines = command.Medicines
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






        }
    }

            public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string TakeFrequency { get; set; } = default!;
        public int ConsultationId { get; set; }
        public int PatientId { get; set; }
        public int WorkerSSIN { get; set; }
        public virtual IList<Medicine> Medicines { get; set; } = new List<Medicine>();
        public virtual Worker Worker { get; set; } = null!;
        public virtual Patient Patient { get; set; } = null!;
        public virtual Consultation Consultation { get; set; } = null!;
