using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyFinalProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFinalProject.Configurations
{
    public class BlogConfiguration : IEntityTypeConfiguration<Blog>
    {
        public void Configure(EntityTypeBuilder<Blog> builder)
        {
            builder.Property(x => x.Date).HasDefaultValueSql("GETUTCDATE()");
            builder.Property(x => x.Title).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Desc).HasMaxLength(500).IsRequired();
            builder.Property(x => x.Image).HasMaxLength(200).IsRequired();
        }
    }
}
