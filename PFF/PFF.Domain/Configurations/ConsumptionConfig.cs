using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PFF.Domain.Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PFF.Domain.Configurations
{
    internal class ConsumptionConfig : IEntityTypeConfiguration<Consumption>
    {
        public void Configure(EntityTypeBuilder<Consumption> builder)
        {
            builder.HasKey(c => c.Id);


            builder.ToTable("Consommation", t =>
            {
                t.HasCheckConstraint("CK_Consommation_Produit", "LEN(TRIM(Produit)) > 0");
                t.HasCheckConstraint("CK_Consommation_Fréquence", "LEN(TRIM(Fréquence)) > 0");
            });


            builder.Property(c => c.DrugType)
                .IsRequired()
                .HasColumnType("NVARCHAR(50)")
                .HasColumnName("Produit")
                .HasConversion<string>();

            builder.Property(c => c.Frequency)
                .IsRequired()
                .HasColumnType("NVARCHAR(128)")
                .HasColumnName("Fréquence")
                .HasConversion<string>();


            builder.HasOne(c => c.Patient)
                .WithMany(p => p.Consumptions)
                .HasForeignKey(c => c.PatientId);
        }
    }
}
