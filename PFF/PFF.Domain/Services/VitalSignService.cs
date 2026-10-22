using Microsoft.EntityFrameworkCore;
using PFF.Domain.Commands.VitalSigns;
using PFF.Domain.Errors;
using PFF.Domain.Model.Entities;
using PFF.Domain.Queries.VitalSigns;
using PFF.Domain.Repositories;
using PFF.Tools.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace PFF.Domain.Services
{
    public class VitalSignService : IVitalSignRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public VitalSignService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<IEnumerable<VitalSign>>> HandleAsync(GetVitalSignsQuery query)
        {
            IList<VitalSign> vitalSigns = await _dbContext.VitalSigns.ToListAsync();
            return Result<IEnumerable<VitalSign>>.Success(vitalSigns);
        }

        public async Task<Result<VitalSign>> HandleAsync(GetVitalSignDetailsQuery query)
        {
            VitalSign? vitalSign = await _dbContext.VitalSigns
                .Include(vs => vs.Consultation)
                .SingleOrDefaultAsync(vs => vs.Id == query.Id);

            if (vitalSign is null)
                return VitalSignErrors.VitalSignNotFound;

            return Result<VitalSign>.Success(vitalSign);
        }

        public async Task<Result> HandleAsync(CreateVitalSignCommand command, CancellationToken cancellationToken)
        {
            try
            {
                VitalSign vitalSign = new VitalSign()
                {
                    HeartRate = command.HeartRate,
                    BloodPressure = command.BloodPressure,
                    Temperature = command.Temperature,
                    OxygenSaturation = command.OxygenSaturation,
                    Height = command.Height,
                    Weight = command.Weight,
                    BloodSugar  = command.BloodSugar,
                    Date = command.Date,
                    ConsultationId = command.ConsultationId,
                };

                vitalSign.BMI = vitalSign.BMICalculator();

                await _dbContext.VitalSigns.AddAsync(vitalSign);
                await _dbContext.SaveChangesAsync();
                return Result.Success();
            }

            catch(Exception ex)
            {
                return VitalSignErrors.VitalSignException;
            }
        }

        public async Task<Result> HandleAsync(UpdateVitalSignCommand command, CancellationToken cancellationToken)
        {
            VitalSign? vitalSign = await _dbContext.VitalSigns.FindAsync(command.Id);

            if (vitalSign is null)
                return VitalSignErrors.VitalSignNotFound;

            if (command.HeartRate > 0)
                vitalSign.HeartRate = command.HeartRate.Value;

            if (!string.IsNullOrWhiteSpace(command.BloodPressure))
                vitalSign.BloodPressure = command.BloodPressure;

            if (command.Temperature > 0)
                vitalSign.Temperature = command.Temperature.Value;

            if (command.OxygenSaturation > 0)
                vitalSign.OxygenSaturation = command.OxygenSaturation.Value;

            if (command.Height > 0)
                vitalSign.Height = command.Height.Value;

            if (command.Weight > 0)
                vitalSign.Weight = command.Weight.Value;

            if (command.BloodSugar > 0)
                vitalSign.BloodSugar = command.BloodSugar.Value;

            if (command.Date is not null)
                vitalSign.Date = command.Date.Value;

            if (command.ConsultationId > 0)
                vitalSign.ConsultationId = command.ConsultationId.Value;

            vitalSign.BMI = vitalSign.BMICalculator();

            await _dbContext.SaveChangesAsync();
            return Result.Success();
        }

        public async Task<Result> HandleAsync(DeleteVitalSignCommand command, CancellationToken cancellationToken)
        {
            VitalSign? vitalSign = await _dbContext.VitalSigns.FindAsync(command.Id);

            if (vitalSign is null)
                return VitalSignErrors.VitalSignNotFound;

            _dbContext.VitalSigns.Remove(vitalSign);
            await _dbContext.SaveChangesAsync();
            return Result.Success();
        }
    }
}