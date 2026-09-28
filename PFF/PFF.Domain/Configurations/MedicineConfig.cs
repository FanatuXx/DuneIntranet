using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PFF.Domain.Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PFF.Domain.Configurations
{
    internal class MedicineConfig : IEntityTypeConfiguration<Medicine>
    {
        public void Configure(EntityTypeBuilder<Medicine> builder)
        {
            builder.HasKey(m => m.Id);


            builder.ToTable("Médicament", t =>
            {
                t.HasCheckConstraint("CK_Médicament_Nom", "LEN(TRIM(Nom)) > 0");
                t.HasCheckConstraint("CK_Médicament_MethodePrise", "LEN(TRIM(Nom)) > 0");
            });


            builder.Property(m => m.Name)
                .IsRequired()
                .HasColumnType("NVARCHAR(128)")
                .HasColumnName("Nom");

            builder.Property(m => m.HowToTake)
                .IsRequired()
                .HasColumnType("NVARCHAR(128)")
                .HasColumnName("MéthodePrise");


            builder.HasIndex(m => m.Name)
                .IsUnique();


            builder.HasMany(m => m.Prescriptions)
                .WithMany(pre => pre.Medicines);

            builder.HasMany(m => m.ChronicTreatments)
                .WithMany(ct => ct.Medicines);
        }
    }
}