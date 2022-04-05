using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyFinalProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFinalProject.Configurations
{
    public class FeatureConfiguration : IEntityTypeConfiguration<Feature>
    {
        public void Configure(EntityTypeBuilder<Feature> builder)
        {
            builder.Property(x => x.Icon).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Title).HasMaxLength(20).IsRequired();
            builder.Property(x => x.Desc).HasMaxLength(80).IsRequired();
        }
    }
}
