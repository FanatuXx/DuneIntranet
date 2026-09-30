using PFF.Domain.Commands;
using PFF.Domain.Errors;
using PFF.Domain.Model.Entities;
using PFF.Domain.Queries;
using PFF.Domain.Repositories;
using PFF.Tools.CommandQuerySeparation;
using PFF.Tools.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace PFF.Domain.Services
{
    public class PatientService : IPatientRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public PatientService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Result<IEnumerable<Patient>> Handle(GetPatientsQuery query)
        {
            return Result<IEnumerable<Patient>>.Success(_dbContext.Patients.AsEnumerable());
        }

        public Result<Patient> Handle(GetPatientDetailsQuery query)
        {
            Patient? patient = _dbContext.Patients.Find(query.Id);

            if (patient is null)
                return PatientErrors.PatientNotFound;

            return Result<Patient>.Success(patient);
        }

        public Result Handle(CreatePatientCommand command)
        {
            try
            {
                Patient patient = new Patient()
                {
                    SSIN = command.SSIN,
                    IdNumber = command.IdNumber,
                    FirstName = command.FirstName,
                    LastName = command.LastName,
                    Alias = command.Alias,
                    Gender = command.Gender,
                    BirthDate = command.BirthDate,
                    PhoneNumber = command.PhoneNumber,
                    Allergies = command.Allergies,
                    IsInsured = command.IsInsured,
                    Insurance = command.Insurance,
                    HasInsuranceCard = command.HasInsuranceCard,
                    InsuranceCardEndDate = command.InsuranceCardEndDate,
                    IsAtFedasil = command.IsAtFedasil,
                    Income = command.Income,
                    Status = command.Status,
                    IsWorking = command.IsWorking,
                    DrugType = command.DrugType,
                    ConsumptionFrequency = command.ConsumptionFrequency,
                    RegistrationDate = DateTime.Now,
                    LastVisit = DateTime.Now
                };
                _dbContext.Add(patient);
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