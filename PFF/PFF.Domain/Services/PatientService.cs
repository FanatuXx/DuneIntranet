using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.DependencyInjection;
using PFF.Domain.Commands.Consumptions;
using PFF.Domain.Commands.Patient;
using PFF.Domain.Commands.Patients;
using PFF.Domain.Commands.VitalSigns;
using PFF.Domain.Errors;
using PFF.Domain.Model.Entities;
using PFF.Domain.Model.ResponseDtos;
using PFF.Domain.Queries.Patients;
using PFF.Domain.Repositories;
using PFF.Tools.Results;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace PFF.Domain.Services
{
    public class PatientService : IPatientRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public PatientService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<Result<IEnumerable<Patient>>> HandleAsync(GetPatientsQuery query)
        {
            List<Patient> patients = await _dbContext.Patients.ToListAsync();
            return Result<IEnumerable<Patient>>.Success(patients);
        }

        public async Task<Result<PatientDetailsDto>> HandleAsync(GetPatientDetailsQuery query)
        {
            Patient? patient = await _dbContext.Patients
                .AsNoTracking()
                .Include(p => p.PatientAddress)
                .Include(p => p.Consumptions)
                .SingleOrDefaultAsync(p => p.Id == query.Id);

            if (patient is null)
                return PatientErrors.PatientNotFound;

            PatientDetailsDto dto = new PatientDetailsDto
            {
                Id = patient.Id,
                SSIN = patient.SSIN,
                IdNumber = patient.IdNumber,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                Alias = patient.Alias,
                Gender = patient.Gender,
                BirthDate = patient.BirthDate,
                PhoneNumber = patient.PhoneNumber,
                Allergies = patient.Allergies,
                IsInsured = patient.IsInsured,
                Insurance = patient.Insurance,
                HasInsuranceCard = patient.HasInsuranceCard,
                InsuranceCardEndDate = patient.InsuranceCardEndDate,
                IsAtFedasil = patient.IsAtFedasil,
                Income = patient.Income,
                Status = patient.Status,
                IsWorking = patient.IsWorking,
                RegistrationDate = patient.RegistrationDate,
                LastVisit = patient.LastVisit,

                PatientAddress = new PatientAddressSummaryDto
                {
                    Id = patient.PatientAddress.Id,
                    Street = patient.PatientAddress.Street,
                    Number = patient.PatientAddress.Number,
                    ZipCode = patient.PatientAddress.ZipCode,
                    Town = patient.PatientAddress.Town,
                    Country = patient.PatientAddress.Country
                },

                Consumptions = patient.Consumptions.Select(c => new ConsumptionSummaryDto
                {
                    Id = c.Id,
                    DrugType = c.DrugType,
                    Frequency = c.Frequency
                }).ToList()
            };

            return Result<PatientDetailsDto>.Success(dto);
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
                    RegistrationDate = DateTime.Now,
                    LastVisit = DateTime.Now,

                    PatientAddress = new PatientAddress()
                    {
                        Street = command.PatientAddress.Street,
                        Number = command.PatientAddress.Number,
                        ZipCode = command.PatientAddress.ZipCode,
                        Town = command.PatientAddress.Town,
                        Country = command.PatientAddress.Country
                    }
                };

                if (command.Consumptions is not null && command.Consumptions.Count > 0)
                {
                    foreach (CreateConsumptionData? consumptionData in command.Consumptions)
                    {
                        Consumption consumption = new Consumption()
                        {
                            DrugType = consumptionData.DrugType,
                            Frequency = consumptionData.Frequency,

                            Patient = patient
                        };

                        patient.Consumptions.Add(consumption); 
                    }
                }

                await _dbContext.AddAsync(patient);
                await _dbContext.SaveChangesAsync();
                return Result.Success();
            }

            catch (Exception ex)
            {
                return PatientErrors.PatientException;
            }
        }


        public async Task <Result> HandleAsync(UpdatePatientCommand command, CancellationToken token)
        {
            Patient? patient = await _dbContext.Patients
                .Include(p => p.PatientAddress)
                .Include(p => p.Consumptions)
                .SingleOrDefaultAsync(p => p.Id == command.Id); //Remplacement du _dbContext.Patient.Find(command.Id) car cette fonction ne permet pas de charger des données liées (ici le PatientAddress), elle permet juste de trouver le Patient avec sa clé primaire

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
                patient.BirthDate = command.BirthDate.Value;

            if (command.Status is not null)
                patient.Status = command.Status;

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

            if(command.PatientAddress is not null)
            {
                patient.PatientAddress.Street = command.PatientAddress.Street ?? patient.PatientAddress.Street;
                patient.PatientAddress.Number = command.PatientAddress.Number ?? patient.PatientAddress.Number;
                patient.PatientAddress.ZipCode = command.PatientAddress.ZipCode ?? patient.PatientAddress.ZipCode;
                patient.PatientAddress.Town = command.PatientAddress.Town ?? patient.PatientAddress.Town;
                patient.PatientAddress.Country = command.PatientAddress.Country ?? patient.PatientAddress.Country;
            }

            IList<Consumption> existingConsumptions = patient.Consumptions!;

            foreach (UpdateConsumptionData data in command.ConsumptionsToUpdate ?? new List<UpdateConsumptionData>())
            {
                Consumption? consumption = existingConsumptions?.SingleOrDefault(vs => vs.Id == data.Id);

                if (consumption is null)
                    return ConsumptionErrors.ConsumptionNotFound;

                consumption.DrugType = data.DrugType ?? consumption.DrugType;                                    // ?? permet d'attribuer la valeur uniquement si elle n'est pas null (la valeur qui précède le ??), sinon on prend la valeur APRES les "??"
                consumption.Frequency = data.Frequency ?? consumption.Frequency;
            }

            foreach (CreateConsumptionData data in command.ConsumptionsToCreate ?? new List<CreateConsumptionData>())
            {
                Consumption consumption = new Consumption
                {
                    DrugType = data.DrugType,
                    Frequency = data.Frequency,

                    Patient = patient
                };

                existingConsumptions!.Add(consumption);
            }


            await _dbContext.SaveChangesAsync();
            return Result.Success();
        }

        public async Task<Result> HandleAsync(DeletePatientCommand command, CancellationToken cancellationToken)
        {
            Patient? patient = await _dbContext.Patients.FindAsync(command.Id);

            if (patient is null)
                return PatientErrors.PatientNotFound;

            _dbContext.Remove(patient);
            await _dbContext.SaveChangesAsync();
            return Result.Success();
        }
    }
}
