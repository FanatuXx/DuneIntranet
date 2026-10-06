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


            builder.ToTable("AdressePatient", t =>
            {
                t.HasCheckConstraint("CK_Adresse_CodePostal", "CodePostal BETWEEN 1000 AND 9999");
            });


            builder.Property(pAddress => pAddress.Street)
                .HasColumnType("NVARCHAR(256)")
                .HasColumnName("Rue")
                .IsRequired(false);

            builder.Property(pAddress => pAddress.Number)
                .HasColumnType("NVARCHAR(10)")
                .HasColumnName("Numéro")
                .IsRequired(false);

            builder.Property(pAddress => pAddress.ZipCode)
                .HasColumnType("INT")
                .HasColumnName("CodePostal")
                .IsRequired();

            builder.Property(pAddress => pAddress.Town)
                .HasColumnType("NVARCHAR(128)")
                .HasColumnName("Ville")
                .IsRequired(false);

            builder.Property(pAddress => pAddress.Country)
                .HasColumnType("NVARCHAR(128)")
                .HasColumnName("Pays")
                .IsRequired(false);


            builder.HasMany(pAddress => pAddress.Patients)
                .WithOne(patient => patient.PatientAddress);
        }
    }
}
