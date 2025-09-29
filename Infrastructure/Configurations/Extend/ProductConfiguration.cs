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
    public class ProductConfiguration : EntityTypeConfiguration<Product, int>
    {
        public override void Configure(EntityTypeBuilder<Product> builder)
        {
            base.Configure(builder); // Gọi cấu hình mặc định từ class cha

            builder.Property(x => x.Name).IsRequired(true).HasMaxLength(200);

            builder.Property(x => x.Code).IsRequired(true).HasMaxLength(50);

            builder.HasIndex(x => x.Code).IsUnique();

            builder.Property(x => x.Note).HasMaxLength(2000);

            builder.Property(x => x.BrandId).IsRequired(true);

            builder.Property(x => x.Price).IsRequired(true);

            builder.HasOne(x => x.Brand).WithMany(x => x.Products).HasForeignKey(x => x.BrandId).HasPrincipalKey(x => x.Id);
        }
    }
}
