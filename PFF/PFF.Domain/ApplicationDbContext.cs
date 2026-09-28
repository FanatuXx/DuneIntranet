using Microsoft.EntityFrameworkCore;
using PFF.Domain.Model.Entities;

namespace PFF.Domain
{
    public class ApplicationDbContext : DbContext
    {
        public virtual DbSet<Worker> Workers { get { return Set<Worker>(); } }
        public virtual DbSet<Patient> Patients { get { return Set<Patient>(); } }
        public virtual DbSet<PatientAddress> PatientAddresses { get { return Set<PatientAddress>(); } }
        public virtual DbSet<Consultation> Consultations { get { return Set<Consultation>(); } }
        public virtual DbSet<Medicine> Medicines { get { return Set<Medicine>(); } }
        public virtual DbSet<Pathology> Pathologies { get { return Set<Pathology>(); } }
        public virtual DbSet<Prescription> Prescriptions { get { return Set<Prescription>(); } }
        public virtual DbSet<VitalSign> VitalSigns { get { return Set<VitalSign>(); } }
        public virtual DbSet<ChronicTreatment> ChronicTreatments { get {  return Set<ChronicTreatment>(); } }

        public ApplicationDbContext(DbContextOptions options) : base (options) 
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}
