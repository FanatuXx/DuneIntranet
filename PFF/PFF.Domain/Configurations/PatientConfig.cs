using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PFF.Domain.Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PFF.Domain.Configurations
{
    internal class PatientConfig : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.HasKey(patient => patient.Id);


            builder.ToTable("Patient", t =>
            {
                t.HasCheckConstraint("CK_Patient_Identification", "LEN(TRIM(Prénom)) > 0 OR LEN(TRIM(Nom)) > 0 OR LEN(TRIM(Surnom)) > 0");
                t.HasCheckConstraint("CK_Patient_DernièreVisite", "DateDernièreVisite >= DateInscription AND DateDernièreVisite <= GETDATE()");
                //t.HasCheckConstraint("CK_Patient_Mutuelle", "Mutuelle IS NULL OR LEN(TRIM(Mutuelle)) > 0 ");
            });


            builder.Property(patient => patient.SSIN)
                .HasColumnType("NVARCHAR(50)")
                .HasColumnName("NISS");

            builder.Property(patient => patient.IdNumber)
                .HasColumnType("NVARCHAR(50)")
                .HasColumnName("NuméroID");

            builder.Property(patient => patient.FirstName)
                .HasColumnType("NVARCHAR(50)")
                .HasColumnName("Prénom");

            builder.Property(patient => patient.LastName)
                .HasColumnType("NVARCHAR(50)")
                .HasColumnName("Nom");

            builder.Property(patient => patient.Alias)
                .IsRequired()
                .HasColumnType("NVARCHAR(50)")
                .HasColumnName("Surnom");

            builder.Property(patient => patient.Gender) 
                .HasColumnType("NVARCHAR(50)")
                .HasColumnName("Genre");

            builder.Property(patient => patient.BirthDate)
                .IsRequired()
                .HasColumnType("DATETIME")
                .HasColumnName("DateNaissance");

            builder.Property(patient => patient.PhoneNumber)
                .HasColumnType("NVARCHAR(50)")
                .HasColumnName("NuméroTéléphone");

            builder.Property(patient => patient.Allergies)
                .HasColumnType("NVARCHAR(256)")
                .HasColumnName("Allergies");

            builder.Property(patient => patient.RegistrationDate)
                .IsRequired()
                .HasColumnType("DATETIME")
                .HasColumnName("DateInscription")
                .HasDefaultValueSql("GETDATE()");

            builder.Property(patient => patient.LastVisit)
                .HasColumnType("DATETIME")
                .HasColumnName("DateDernièreVisite")
                .HasDefaultValueSql("GETDATE()");

            builder.Property(patient => patient.IsInsured)
                .HasColumnType("BIT")
                .HasColumnName("Assuré");

            builder.Property(patient => patient.Insurance)
                .HasColumnType("NVARCHAR(128)")
                .HasColumnName("Mutuelle");

            builder.Property(patient => patient.HasInsuranceCard)
                .HasColumnType("BIT")
                .HasColumnName("CarteMédicale");

            builder.Property(patient => patient.InsuranceCardEndDate)
                .HasColumnType("DATETIME")
                .HasColumnName("ExpirationCarteMédicale");

            builder.Property(patient => patient.IsAtFedasil)
                .HasColumnType("BIT")
                .HasColumnName("Fedasil");

            builder.Property(patient => patient.DrugType)
                .HasColumnType("NVARCHAR(50)")
                .HasColumnName("ProduitConsommé")
                .HasConversion<string>();

            builder.Property(patient => patient.ConsumptionFrequency)
                .HasColumnType("NVARCHAR(128)")
                .HasColumnName("FréquenceConsommation")
                .HasConversion<string>();

            builder.Property(patient => patient.Income)
                .HasColumnType("INT")
                .HasColumnName("Revenus");

            builder.Property(patient => patient.Status)
                .HasColumnType("NVARCHAR(128)")
                .HasColumnName("Statut");

            builder.Property(patient => patient.IsWorking)
                .HasColumnType("BIT")
                .HasColumnName("Travail");


            builder.HasIndex(patient => patient.SSIN)
                .IsUnique();


            builder.HasOne(patient => patient.PatientAddress)
                .WithMany(pAddress => pAddress.Patients)
                .HasForeignKey(patient => patient.PatientAddressId);

            builder.HasMany(patient => patient.Prescriptions)
                .WithOne(pre => pre.Patient);

            builder.HasMany(patient => patient.Consultations)
                .WithOne(c => c.Patient);
        }
    }
}