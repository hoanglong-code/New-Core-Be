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
    public class FunctionRoleConfiguration : EntityTypeConfiguration<FunctionRole, int>
    {
        public override void Configure(EntityTypeBuilder<FunctionRole> builder)
        {
            base.Configure(builder); // Gọi cấu hình mặc định từ class cha

            builder.Property(x => x.FunctionId).IsRequired(true);

            builder.Property(x => x.RoleId).IsRequired(true);

            builder.Property(x => x.ActiveKey).IsRequired(true);

            builder.HasOne(x => x.Function).WithMany(x => x.FunctionRole).HasForeignKey(x => x.FunctionId).HasPrincipalKey(x => x.Id);

            builder.HasOne(x => x.Role).WithMany(x => x.FunctionRole).HasForeignKey(x => x.RoleId).HasPrincipalKey(x => x.Id);
        }
    }
}
