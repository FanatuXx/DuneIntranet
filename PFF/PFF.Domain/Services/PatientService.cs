using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PFF.Domain.Commands.Patient;
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

        public async Task<Result> HandleAsync(CreatePatientCommand command, CancellationToken cancellationToken)
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
                    LastVisit = DateTime.Now,
                    PatientAddress = new PatientAddress()
                    {
                        Street = command.Street,
                        Number = command.Number,
                        ZipCode = command.ZipCode,
                        Town = command.Town,
                        Country = command.Country
                    }
                };

                await _dbContext.AddAsync(patient);
                await _dbContext.SaveChangesAsync();
                return Result.Success();
            }

            catch (Exception ex)
            {
                return PatientErrors.PatientException;
            }
        }


        public Result Handle(UpdatePatientCommand command)
        {
            Patient? patient = _dbContext.Patients 
                .Include(p => p.PatientAddress)
                .SingleOrDefault(p => p.Id == command.Id); //Remplacement du _dbContext.Patient.Find(command.Id) car cette fonction ne permet pas de charger des données liées (ici le PatientAddress), elle permet juste de trouver le Patient avec sa clé primaire 

            if (patient is null)
                return PatientErrors.PatientNotFound;

            if (!string.IsNullOrWhiteSpace(command.SSIN))
                patient.SSIN = command.SSIN;

            if (!string.IsNullOrWhiteSpace(command.Alias))
                patient.Alias = command.Alias;

            if (!string.IsNullOrWhiteSpace(command.FirstName))
                patient.FirstName = command.FirstName;

            if (!string.IsNullOrWhiteSpace(command.LastName))
                patient.LastName = command.LastName;

            if (command.Gender is not null)
                patient.Gender = command.Gender;

            if (command.BirthDate is not null)
                patient.BirthDate = Convert.ToDateTime(command.BirthDate);

            if (command.Status is not null)
                patient.Status = command.Status;

            if (command.DrugType is not null)
                patient.DrugType = command.DrugType;

            if (command.IsInsured is not null)
                patient.IsInsured = command.IsInsured;

            if (!string.IsNullOrWhiteSpace(command.Insurance))
                patient.Insurance = command.Insurance;

            if (!string.IsNullOrWhiteSpace(command.IdNumber))
                patient.IdNumber = command.IdNumber;

            if (command.IsWorking is not null)
                patient.IsWorking = command.IsWorking;

            if (command.IsAtFedasil is not null)
                patient.IsAtFedasil = command.IsAtFedasil;

            if (!string.IsNullOrWhiteSpace(command.Allergies))
                patient.Allergies = command.Allergies;

            if (command.Income is not null)
                patient.Income = command.Income;

            if (command.InsuranceCardEndDate is not null)
                patient.InsuranceCardEndDate = command.InsuranceCardEndDate;

            if (command.HasInsuranceCard is not null)
                patient.HasInsuranceCard = command.HasInsuranceCard;

            if (command.ConsumptionFrequency is not null)
                patient.ConsumptionFrequency = command.ConsumptionFrequency;

            if (!string.IsNullOrWhiteSpace(command.Street))
                patient.PatientAddress.Street = command.Street;

            if (!string.IsNullOrWhiteSpace(command.Number))
                patient.PatientAddress.Number = command.Number;

            if (!string.IsNullOrWhiteSpace(command.ZipCode))
                patient.PatientAddress.ZipCode = command.ZipCode;

            if (!string.IsNullOrWhiteSpace(command.Town))
                patient.PatientAddress.Town = command.Town;

            if (!string.IsNullOrWhiteSpace(command.Country))
                patient.PatientAddress.Country = command.Country;


            _dbContext.SaveChanges();
            return Result.Success();
        }

        public Result Handle(DeletePatientCommand command)
        {
            Patient? patient = _dbContext.Patients.Find(command.Id);

            if (patient is null)
                return PatientErrors.PatientNotFound;

            _dbContext.Remove(patient);
            _dbContext.SaveChanges();
            return Result.Success();
        }
    }
}