using Microsoft.AspNetCore.Mvc;
using PFF.Api.Dtos.Create;
using PFF.Api.Dtos.Update;
using PFF.Api.Infrastructure;
using PFF.Domain.Commands.Consultations;
using PFF.Domain.Commands.Prescriptions;
using PFF.Domain.Commands.VitalSigns;
using PFF.Domain.Queries.Consultations;
using PFF.Domain.Repositories;
using PFF.Tools.Results;

namespace PFF.Api.Controllers
{
    [ApiController]
    [Route("api/consultations")]
    public class ConsultationController : ControllerBase
    {
        private readonly IConsultationRepository _consultationRepository;

        public ConsultationController(IConsultationRepository consultationRepository)
        {
            _consultationRepository = consultationRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _consultationRepository.HandleAsync(new GetConsultationsQuery());
            return this.FromResult(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDetails(int id)
        {
            var result = await _consultationRepository.HandleAsync(new GetConsultationDetailsQuery(id));
            return this.FromResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateConsultationDto dto)
        {
            Result result = await _consultationRepository.HandleAsync(new CreateConsultationCommand(
                dto.Motive,
                dto.Subjective,
                dto.Objective,
                dto.Assessment,
                dto.Plan,
                dto.Description,
                dto.Date,
                dto.PatientId,
                dto.WorkersIds,
                dto.PathologiesIds,

                (dto.Prescriptions ?? new List<CreatePrescriptionDto>())    //Si dto.Prescription est null, on crée une liste vide 
                    .Select(p => new CreatePrescriptionData(                //On transforme les DTO reçus de l'API en objets de commande
                        p.StartDate,
                        p.EndDate,
                        p.TakeFrequency,
                        p.PrescriberId,
                        p.MedicinesIds))
                    .ToList(),                                              //puis on rassemble ces objets dans une liste compatible avec la commande métier.

                (dto.VitalSigns ?? new List<CreateVitalSignDto>())
                    .Select(vs => new CreateVitalSignData(
                        vs.HeartRate,
                        vs.BloodPressure,
                        vs.Temperature,
                        vs.OxygenSaturation,
                        vs.Height,
                        vs.Weight,
                        vs.BloodSugar,
                        dto.Date))
                    .ToList()), CancellationToken.None);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Created($"https://localhost:7050/api/consultation", null);
        }

        [HttpPatch("{id}")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateConsultationDto dto)
        {
            Result result = await _consultationRepository.HandleAsync(new UpdateConsultationCommand(
                id,
                dto.Motive,
                dto.Subjective,
                dto.Objective,
                dto.Assessment,
                dto.Plan,
                dto.Description,
                dto.Date,
                dto.PatientId,
                dto.WorkersIds,
                dto.PathologiesIds,

                (dto.PrescriptionsToUpdate ?? new List<UpdatePrescriptionDto>())
                    .Select(p => new UpdatePrescriptionData(
                        id,
                        p.StartDate,
                        p.EndDate,
                        p.TakeFrequency,
                        p.PrescriberId,
                        p.MedicinesIds))
                    .ToList(),

                (dto.PrescriptionsToCreate ?? new List<CreatePrescriptionDto>())
                    .Select(p => new CreatePrescriptionData(
                        p.StartDate,
                        p.EndDate,
                        p.TakeFrequency,
                        p.PrescriberId,
                        p.MedicinesIds))
                    .ToList(),

                (dto.VitalSignsToUpdate ?? new List<UpdateVitalSignDto>())
                    .Select(p => new UpdateVitalSignData(
                        id,
                        p.HeartRate,
                        p.BloodPressure,
                        p.Temperature,
                        p.OxygenSaturation,
                        p.Height,
                        p.Weight,
                        p.BloodSugar,
                        p.Date))
                    .ToList(),

                (dto.VitalSignsToCreate ?? new List<CreateVitalSignDto>())
                    .Select(p => new CreateVitalSignData(
                        p.HeartRate,
                        p.BloodPressure,
                        p.Temperature,
                        p.OxygenSaturation,
                        p.Height,
                        p.Weight,
                        p.BloodSugar,
                        DateTime.Now))
                    .ToList()), CancellationToken.None);

            return this.FromResult(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Result result = await _consultationRepository.HandleAsync(new DeleteConsultationCommand(id), CancellationToken.None);
            return this.FromResult(result);
        }
    }
}
