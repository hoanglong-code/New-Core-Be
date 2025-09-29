using Domain.Entities.Extend;
using Infrastructure.Configurations.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configurations.Extend
{
    public class UserConfiguration : EntityTypeConfiguration<User, int>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            base.Configure(builder); // Gọi cấu hình mặc định từ class cha

            builder.Property(e => e.Address).IsRequired(true).HasMaxLength(1000);

            builder.Property(e => e.Code).IsRequired(true).HasMaxLength(50);

            builder.Property(e => e.Email).IsRequired(true).HasMaxLength(1000).IsUnicode(false);

            builder.Property(e => e.FullName).IsRequired(true).HasMaxLength(100);

            builder.Property(e => e.KeyLock).IsRequired(true).HasMaxLength(8);

            builder.Property(e => e.Password).IsRequired(true).HasMaxLength(50).IsUnicode(false);

            builder.Property(e => e.Phone).IsRequired(true).HasMaxLength(50).IsUnicode(false);

            builder.Property(e => e.RegEmail).IsRequired(true).HasMaxLength(50);

            builder.Property(e => e.LastLoginAt).HasColumnType("datetime");

            builder.Property(e => e.UserName).IsRequired(true).HasMaxLength(50);

            builder.HasIndex(x => x.UserName).IsUnique();
        }
    }
}
