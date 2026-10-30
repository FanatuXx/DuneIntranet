using Microsoft.EntityFrameworkCore;
using PFF.Domain.Commands.Consultations;
using PFF.Domain.Commands.Prescriptions;
using PFF.Domain.Commands.VitalSigns;
using PFF.Domain.Errors;
using PFF.Domain.Model.Entities;
using PFF.Domain.Queries.Consultations;
using PFF.Domain.Repositories;
using PFF.Tools.Results;
using PFF.Domain.Model.ResponseDtos;

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

        public async Task<Result<ConsultationDetailsDto>> HandleAsync(GetConsultationDetailsQuery query)
        {
            Consultation? consultation = await _dbContext.Consultations
                .AsNoTracking()
                .Include(c => c.Patient)
                .Include(c => c.Workers)
                .Include(c => c.Pathologies)
                .Include(c => c.VitalSigns)
                .Include(c => c.Prescriptions)
                    .ThenInclude(pre => pre.Medicines)
                .SingleOrDefaultAsync(c => c.Id == query.Id);

            if (consultation is null)
                return ConsultationErrors.ConsultationNotFound;

            ConsultationDetailsDto dto = new ConsultationDetailsDto
            {
                Id = consultation.Id,
                Motive = consultation.Motive,
                Subjective = consultation.Subjective,
                Objective = consultation.Objective,
                Assessment = consultation.Assessment,
                Plan = consultation.Plan,
                Description = consultation.Description,
                Date = consultation.Date,

                Patient = consultation.Patient is null ? null : new PatientSummaryDto
                {
                    Id = consultation.Patient.Id,
                    SSIN = consultation.Patient.SSIN,
                    IdNumber = consultation.Patient.IdNumber,
                    FirstName = consultation.Patient.FirstName,
                    LastName = consultation.Patient.LastName,
                    Alias = consultation.Patient.Alias,
                    Gender = consultation.Patient.Gender,
                    BirthDate = consultation.Patient.BirthDate,
                    PhoneNumber = consultation.Patient.PhoneNumber,
                    Allergies = consultation.Patient.Allergies,
                    IsInsured = consultation.Patient.IsInsured,
                    Insurance = consultation.Patient.Insurance,
                    HasInsuranceCard = consultation.Patient.HasInsuranceCard,
                    InsuranceCardEndDate = consultation.Patient.InsuranceCardEndDate,
                    IsAtFedasil = consultation.Patient.IsAtFedasil,
                    Income = consultation.Patient.Income,
                    Status = consultation.Patient.Status,
                    IsWorking = consultation.Patient.IsWorking,
                    RegistrationDate = consultation.Patient.RegistrationDate,
                    LastVisit = consultation.Patient.LastVisit,

                    Consumptions = consultation.Patient.Consumptions?.Select(c => new ConsumptionSummaryDto
                    {
                        Id = c.Id,
                        DrugType = c.DrugType,
                        Frequency = c.Frequency
                    }).ToList()
                },

                Workers = consultation.Workers.Select(w => new WorkerSummaryDto
                {
                    Id = w.Id,
                    FirstName = w.FirstName,
                    LastName = w.LastName,
                    Occupation = w.Occupation,
                    Email = w.Email
                }).ToList(),

                Pathologies = consultation.Pathologies.Select(p => new PathologySummaryDto
                {
                    Id = p.Id,
                    Name = p.Name
                }).ToList(),

                VitalSigns = consultation.VitalSigns?.Select(vs => new VitalSignSummaryDto
                {
                    Id = vs.Id,
                    HeartRate = vs.HeartRate,
                    BloodPressure = vs.BloodPressure,
                    Temperature = vs.Temperature,
                    OxygenSaturation = vs.OxygenSaturation,
                    BMI = vs.BMI,
                    BloodSugar = vs.BloodSugar,
                }).ToList(),

                Prescriptions = consultation.Prescriptions?.Select(pre => new PrescriptionWithMedicineDto
                {
                    Id = pre.Id,
                    StartDate = pre.StartDate,
                    EndDate = pre.EndDate,
                    TakeFrequency = pre.TakeFrequency,
                    WorkerId = pre.WorkerId,
                    Medicines = pre.Medicines?.Select(m => new MedicineSummaryDto
                    {
                        Id = m.Id,
                        Name = m.Name,
                        HowToTake = m.HowToTake
                    }).ToList()
                }).ToList(),
            };

            return Result<ConsultationDetailsDto>.Success(dto);
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

                    vitalSign.BMI = vitalSign.BMICalculator();
                    consultation.VitalSigns!.Add(vitalSign);
                }

                foreach (CreatePrescriptionData prescriptionData in command.Prescriptions) //Boucle dans la liste de prescriptions et ajoute chacune d'elle dans la consultation
                {
                    IList<int> medicineIds = (prescriptionData.MedicinesIds ?? new List<int>())
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


            IList<Prescription> existingPrescriptions = consultation.Prescriptions!;

            foreach (UpdatePrescriptionData data in command.PrescriptionsToUpdate ?? new List<UpdatePrescriptionData>())
            {
                Prescription? prescription = existingPrescriptions?.SingleOrDefault(p => p.Id == data.Id);

                if (prescription is null)
                    return PrescriptionErrors.PrescriptionNotFound;

                prescription.StartDate = data.StartDate ?? prescription.StartDate;                                    // ?? permet d'attribuer la valeur uniquement si elle n'est pas null (la valeur qui précède le ??), sinon on prend la valeur APRES les "??"
                prescription.EndDate = data.EndDate ?? prescription.EndDate;
                prescription.TakeFrequency = data.TakeFrequency ?? prescription.TakeFrequency;
                prescription.WorkerId = data.PrescriberId ?? prescription.WorkerId;

                if (data.PrescriberId > 0)
                {
                    Worker? worker = await _dbContext.Workers.FindAsync(data.PrescriberId);

                    if (worker is null)
                        return WorkerErrors.WorkerNotFound;

                    prescription.Worker = worker;
                }

                if (data.MedicinesIds?.Count > 0)
                {
                    IList<Medicine>? medicines = await _dbContext.Medicines
                        .Where(p => data.MedicinesIds.Contains(p.Id))
                        .ToListAsync();

                    if (medicines.Count != data.MedicinesIds?.Distinct().Count())
                    {
                        return MedicineErrors.MedicineNotFound;
                    }

                    prescription.Medicines = medicines;
                }
            }

            foreach (CreatePrescriptionData data in command.PrescriptionsToCreate ?? new List<CreatePrescriptionData>())
            {
                Worker? worker = await _dbContext.Workers.FindAsync(data.PrescriberId);

                if (worker is null)
                    return WorkerErrors.WorkerNotFound;

                Prescription prescription = new Prescription
                {

                    StartDate = data.StartDate,
                    EndDate = data.EndDate,
                    TakeFrequency = data.TakeFrequency,
                    Worker = worker,
                    Patient = consultation.Patient,
                    Consultation = consultation
                };

                if (data.MedicinesIds?.Count > 0)
                {
                    IList<Medicine>? medicines = await _dbContext.Medicines
                        .Where(p => data.MedicinesIds.Contains(p.Id))
                        .ToListAsync();

                    if (medicines.Count != data.MedicinesIds?.Distinct().Count())
                    {
                        return MedicineErrors.MedicineNotFound;
                    }

                    prescription.Medicines = medicines;
                }

                existingPrescriptions!.Add(prescription);
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