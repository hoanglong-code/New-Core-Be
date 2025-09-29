using Domain.Entities.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Enums.ConstantEnums;

namespace Infrastructure.Configurations.Base
{
    public class EntityTypeConfiguration<T, TId> : IEntityTypeConfiguration<T> where T : BaseEntity<TId>, IEntity<TId>
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(t => t.Id).ValueGeneratedOnAdd();

            builder.Property(i => i.Status).HasDefaultValue(EntityStatus.NORMAL);

            builder.Property(x => x.CreatedAt).IsRequired(true).HasColumnType("datetime").HasDefaultValueSql("getdate()");

            builder.Property(x => x.UpdatedAt).IsRequired(true).HasColumnType("datetime").HasDefaultValueSql("getdate()");

            builder.HasQueryFilter(i => i.Status != EntityStatus.DELETED);
        }
    }
}
