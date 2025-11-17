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

            builder.Property(e => e.FullName).IsRequired(true).HasMaxLength(200);

            builder.Property(e => e.UserName).IsRequired(true).HasMaxLength(100);

            builder.HasIndex(x => x.UserName).IsUnique();

            builder.Property(e => e.Password).IsRequired(true).HasMaxLength(100).IsUnicode(false);

            builder.Property(e => e.Email).IsRequired(false).HasMaxLength(200).IsUnicode(false);

            builder.Property(e => e.Phone).IsRequired(false).HasMaxLength(50).IsUnicode(false);

            builder.Property(e => e.CardId).IsRequired(false).HasMaxLength(20);

            builder.Property(e => e.Address).IsRequired(false).HasMaxLength(500);

            builder.Property(e => e.Avatar).IsRequired(false).HasMaxLength(500);

            builder.Property(e => e.Birthday).IsRequired(false).HasColumnType("datetime");

            builder.Property(e => e.LastLoginAt).IsRequired(false).HasColumnType("datetime");

            builder.HasMany(x => x.UserRole).WithOne(x => x.User).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
