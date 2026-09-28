using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PFF.Domain.Model.Entities;

namespace PFF.Domain.Configurations
{
    internal class PatientAddressConfig : IEntityTypeConfiguration<PatientAddress>
    {
        public void Configure(EntityTypeBuilder<PatientAddress> builder)
        {
            builder.HasKey(pAddress => pAddress.Id);


            builder.ToTable("AddressePatient", t =>
            {
                
            });


            builder.Property(pAddress => pAddress.Street)
                .HasColumnType("NVARCHAR(256)")
                .HasColumnName("Rue");

            builder.Property(pAddress => pAddress.Number)
                .HasColumnType("NVARCHAR(10)")
                .HasColumnName("Numéro");

            builder.Property(pAddress => pAddress.ZipCode)
                .IsRequired()
                .HasColumnType("INT")
                .HasColumnName("CodePostal");

            builder.Property(pAddress => pAddress.Town)
                .HasColumnType("NVARCHAR(128)")
                .HasColumnName("Ville");

            builder.Property(pAddress => pAddress.Country)
                .HasColumnType("NVARCHAR(128)")
                .HasColumnName("Pays");


            builder.HasMany(pAddress => pAddress.Patients)
                .WithOne(patient => patient.PatientAddress);
        }
    }
}
