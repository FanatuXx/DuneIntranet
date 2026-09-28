using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PFF.Domain.Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PFF.Domain.Configurations
{
    internal class WorkerConfig : IEntityTypeConfiguration<Worker>
    {
        public void Configure(EntityTypeBuilder<Worker> builder)
        {

            builder.HasKey(w => w.SSIN);


            builder.ToTable("Travailleur", t =>
            {
                t.HasCheckConstraint("CK_Travailleur_Noms", "LEN(TRIM(Prénom)) > 0 AND LEN(TRIM(Nom)) > 0");
                t.HasCheckConstraint("CK_Travailleur_Email", "Email LIKE '%_@__%.__%'");
            });


            builder.Property(w => w.FirstName)
                .IsRequired()
                .HasColumnType("NVARCHAR(50)")
                .HasColumnName("Prénom");

            builder.Property(w => w.LastName)
                .IsRequired()
                .HasColumnType("NVARCHAR(50)")
                .HasColumnName("Nom");

            builder.Property(w => w.Gender)
                .IsRequired()
                .HasColumnType("NVARCHAR(30)")
                .HasColumnName("Genre");

            builder.Property(w => w.BirthDate)
                .IsRequired()
                .HasColumnType("DATETIME")
                .HasColumnName("DateNaissance");

            builder.Property(w => w.Email)
                .IsRequired()
                .HasColumnType("NVARCHAR(256)")
                .HasColumnName("Email");

            builder.Property(w => w.PhoneNumber)
                .HasColumnType("NVARCHAR(50)")
                .HasColumnName("NuméroTéléphone");

            builder.Property(w => w.Occupation)
                .IsRequired()
                .HasColumnType("NVARCHAR(128)")
                .HasColumnName("Fonction");

            builder.Property(w => w.Street)
                .IsRequired()
                .HasColumnType("NVARCHAR(256)")
                .HasColumnName("Rue");

            builder.Property(w => w.Number)
                .IsRequired()
                .HasColumnType("NVARCHAR(10)")
                .HasColumnName("Numéro");

            builder.Property(w => w.ZipCode)
                .IsRequired()
                .HasColumnType("INT")
                .HasColumnName("CodePostal");

            builder.Property(w => w.Town)
                .IsRequired()
                .HasColumnType("NVARCHAR(128)")
                .HasColumnName("Ville");

            builder.Property(w => w.Country)
                .IsRequired()
                .HasColumnType("NVARCHAR(128)")
                .HasColumnName("Pays");


            builder.HasMany(w => w.Prescriptions)
                .WithOne(pre => pre.Worker);

            builder.HasMany(w => w.Consultations)
                .WithMany(c => c.Workers);
        }
    }
}
