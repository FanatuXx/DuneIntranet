using PFF.Domain.Repositories;

namespace PFF.Domain.Services
{
    public class PatientAddressService : IPatientAddressRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public PatientAddressService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
