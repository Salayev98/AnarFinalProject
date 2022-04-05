using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyFinalProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFinalProject.Configurations
{
    public class SliderConfiguration : IEntityTypeConfiguration<Slider>
    {
        public void Configure(EntityTypeBuilder<Slider> builder)
        {
            builder.Property(x => x.Image).HasMaxLength(200).IsRequired();
            builder.Property(x => x.Title1).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Title2).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Desc1).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Desc2).HasMaxLength(100).IsRequired();
            builder.Property(x => x.BtnUrl).HasMaxLength(200).IsRequired();
            builder.Property(x => x.BtnText).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Order).IsRequired();

        }
    }
}
