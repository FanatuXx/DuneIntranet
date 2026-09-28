using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PFF.Domain.Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PFF.Domain.Configurations
{
    internal class PathologyConfig : IEntityTypeConfiguration<Pathology>
    {
        public void Configure(EntityTypeBuilder<Pathology> builder)
        {
            builder.HasKey(patho => patho.Id);

            builder.ToTable("Pathologie", t =>
            {
                t.HasCheckConstraint("CK_Pathologie_Nom", "LEN(TRIM(Nom)) > 0");
            });

            builder.Property(patho => patho.Name)
                .IsRequired()
                .HasColumnType("NVARCHAR(128)")
                .HasColumnName("Nom");


            builder.HasIndex(patho => patho.Name)
                .IsUnique();


            builder.HasMany(patho => patho.Consultations)
                .WithMany(c => c.Pathologies);
        }
    }
}
