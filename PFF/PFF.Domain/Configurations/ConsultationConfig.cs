using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PFF.Domain.Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PFF.Domain.Configurations
{
    internal class ConsultationConfig : IEntityTypeConfiguration<Consultation>
    {
        public void Configure(EntityTypeBuilder<Consultation> builder)
        {
            builder.HasKey(c => c.Id);


            builder.ToTable("Consultation", t =>
            {
                t.HasCheckConstraint("CK_Consultation_Date", "Date <= GETDATE()");
            });


            builder.Property(c => c.Motive)
                .HasColumnType("NVARCHAR(256)")
                .HasColumnName("Motif");

            builder.Property(c => c.Subjective)
                .HasColumnType("NVARCHAR(256)")
                .HasColumnName("Subjectif");

            builder.Property(c => c.Objective)
                .HasColumnType("NVARCHAR(256)")
                .HasColumnName("Objectif");

            builder.Property(c => c.Assessment)
                .HasColumnType("NVARCHAR(256)")
                .HasColumnName("Évaluation");

            builder.Property(c => c.Plan)
                .HasColumnType("NVARCHAR(256)")
                .HasColumnName("Plan");

            builder.Property(c => c.Description)
                .HasColumnType("NVARCHAR(256)")
                .HasColumnName("Description");

            builder.Property(c => c.Date)
                .IsRequired()
                .HasColumnType("DATETIME")
                .HasColumnName("Date");


            builder.HasOne(c => c.Patient)
                .WithMany(patient => patient.Consultations)
                .HasForeignKey(c => c.PatientId);

            builder.HasMany(c => c.Workers)
                .WithMany(w => w.Consultations)
                .UsingEntity("Consultations_Travailleurs_JoinTable");

            builder.HasMany(c => c.Prescriptions)
                .WithOne(pr => pr.Consultation);

            builder.HasMany(c => c.Pathologies)
                .WithMany(patho => patho.Consultations)
                .UsingEntity("Consultations_Pathologies_JoinTable");

            builder.HasMany(c => c.VitalSigns)
                .WithOne(vs => vs.Consultation);
        }
    }
}