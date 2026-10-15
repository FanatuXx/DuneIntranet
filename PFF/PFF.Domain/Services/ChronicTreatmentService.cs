using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PFF.Domain.Model.Entities;
using PFF.Domain.Queries.ChronicTreatments;
using PFF.Domain.Repositories;
using PFF.Tools.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace PFF.Domain.Services
{
    public class ChronicTreatmentService : IChronicTreatmentRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ChronicTreatmentService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<IEnumerable<ChronicTreatment>>> HandleAsync(GetChronicTreatmentsQuery query)
        {
            List<ChronicTreatment> treatments = await _dbContext.ChronicTreatments.ToListAsync();

            return Result<IEnumerable<ChronicTreatment>>.Success(treatments); 
        }

        public async Task<Result<ChronicTreatment>> HandleAsync(GetChronicTreatmentDetailsQuery query)
        {
            ChronicTreatment? treatment = await _dbContext.ChronicTreatments
                .Include(ct => ct.Patient)
                .Include(ct => ct.Pathology)
                .Include(ct => ct.Medicines)
                .SingleOrDefault(ct => ct.Id == query.Id);
        }
    }
}
