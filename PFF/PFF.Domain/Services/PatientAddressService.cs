using Microsoft.EntityFrameworkCore;
using PFF.Domain.Commands.PatientAddress;
using PFF.Domain.Errors;
using PFF.Domain.Model.Entities;
using PFF.Domain.Repositories;
using PFF.Tools.Results;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace PFF.Domain.Services
{
    public class PatientAddressService : IPatientAddressRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public PatientAddressService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Result Handle(CreatePatientAddressCommand command)
        {
            Patient? patient = _dbContext.Patients
                .Include(p => p.PatientAddress)
                .FirstOrDefault(p => p.Id == command.PatientId);

            if (patient is null)
                return PatientErrors.PatientNotFound;

            if (patient.PatientAddress is not null)
                return PatientErrors.PatientAddressAlreadyExists;

            try
            {
                patient.PatientAddress = new PatientAddress()
                {
                    Street = command.Street,
                    Number = command.Number,
                    ZipCode = command.ZipCode,
                    Town = command.Town,
                    Country = command.Country,
                };

                _dbContext.SaveChanges();
                return Result.Success();
            }

            catch (Exception ex)
            {
                return PatientErrors.PatientException;
            }
        }
    }
}
