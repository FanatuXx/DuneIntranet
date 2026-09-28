using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PFF.Domain.Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PFF.Domain.Configurations
{
    internal class ChronicTreatmentConfig : IEntityTypeConfiguration<ChronicTreatment>
    {
        public void Configure(EntityTypeBuilder<ChronicTreatment> builder)
        {
            builder.HasKey(ct => ct.Id);


            builder.ToTable("TTChronique", t =>
            {
                t.HasCheckConstraint("CK_TTChronique_Posologie", "LEN(Posologie) > 0");
            });


            builder.Property(ct => ct.Dosage)
                .IsRequired()
                .HasColumnType("NVARCHAR(256)")
                .HasColumnName("Posologie");


            builder.HasOne(ct => ct.Patient)
                .WithMany(patient => patient.ChronicTreatments)
                .HasForeignKey(ct => ct.PatientId);

            builder.HasOne(ct => ct.Pathology)
                .WithMany(patho => patho.ChronicTreatments)
                .HasForeignKey(ct => ct.PathologyId);

            builder.HasMany(ct => ct.Medicines)
                .WithMany(m => m.ChronicTreatments)
                .UsingEntity("TTChronique_Médicaments_JoinTable");


        }
    }
}
