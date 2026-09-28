using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PFF.Domain.Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PFF.Domain.Configurations
{
    internal class VitalSignConfig : IEntityTypeConfiguration<VitalSign>
    {
        public void Configure(EntityTypeBuilder<VitalSign> builder)
        {
            builder.HasKey(vsm => vsm.Id);


            builder.ToTable("ParamètresVitaux", t =>
            {
                t.HasCheckConstraint("CK_Paramètres_FréquenceCardiaque", "FréquenceCardiaque <= 220");
                t.HasCheckConstraint("CK_Paramètres_Température", "Température >= 30 AND Température <= 45");
                t.HasCheckConstraint("CK_Paramètres_Saturation", "Saturation >= 1 AND Saturation <= 100");
            });


            builder.Property(vs => vs.HeartRate)
                .HasColumnType("INT")
                .HasColumnName("FréquenceCardiaque");

            builder.Property(vs => vs.BloodPressure)
                .HasColumnType("NVARCHAR(50)")
                .HasColumnName("PressionArtérielle");

            builder.Property(vs => vs.Temperature)
                .HasColumnType("DECIMAL(3, 1)")
                .HasColumnName("Température");

            builder.Property(vs => vs.OxygenSaturation)
                .HasColumnType("INT")
                .HasColumnName("Saturation");

            builder.Property(vs => vs.Height)
                .HasColumnType("FLOAT")
                .HasColumnName("Taille");

            builder.Property(vs => vs.Weight)
                .HasColumnType("INT")
                .HasColumnName("Poids");

            builder.Property(vs => vs.BMI)
                .HasColumnType("FLOAT")
                .HasColumnName("IMC");

            builder.Property(vs => vs.BloodSugar)
                .HasColumnType("FLOAT")
                .HasColumnName("Glycémie");


            builder.HasOne(vs => vs.Consultation)
                .WithMany(c => c.VitalSigns)
                .HasForeignKey(vs => vs.ConsultationId);
        }
    }
}
