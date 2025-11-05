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
    public class UserRoleConfiguration : EntityTypeConfiguration<UserRole, int>
    {
        public override void Configure(EntityTypeBuilder<UserRole> builder)
        {
            base.Configure(builder); // Gọi cấu hình mặc định từ class cha

            builder.Property(x => x.UserId).IsRequired(true);

            builder.Property(x => x.RoleId).IsRequired(true);

            builder.HasOne(x => x.User).WithMany(x => x.UserRole).HasForeignKey(x => x.UserId).HasPrincipalKey(x => x.Id);

            builder.HasOne(x => x.Role).WithMany(x => x.UserRole).HasForeignKey(x => x.RoleId).HasPrincipalKey(x => x.Id);
        }
    }
}
