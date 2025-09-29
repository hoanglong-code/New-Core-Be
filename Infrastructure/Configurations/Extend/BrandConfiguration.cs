using Domain.Entities.Extend;
using Infrastructure.Configurations.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configurations.Extend
{
    public class BrandConfiguration : EntityTypeConfiguration<Brand, int>
    {
        public override void Configure(EntityTypeBuilder<Brand> builder)
        {
            base.Configure(builder); // Gọi cấu hình mặc định từ class cha

            builder.Property(x => x.Name).IsRequired(true).HasMaxLength(200);

            builder.Property(x => x.Code).IsRequired(true).HasMaxLength(50);

            builder.HasIndex(x => x.Code).IsUnique();

            builder.Property(x => x.Note).HasMaxLength(2000);

            builder.HasMany(x => x.Products).WithOne(x => x.Brand).HasForeignKey(x => x.BrandId);
        }
    }
}
