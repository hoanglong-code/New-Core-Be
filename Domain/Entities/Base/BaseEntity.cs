using static Domain.Enums.ConstantEnums;

namespace Domain.Entities.Base
{
    public class BaseEntity<TId> : IEntity<TId>
    {
        public TId Id { get; set; }
        public virtual long? CreatedById { get; set; }
        public virtual long? UpdatedById { get; set; }
        public virtual string? CreatedBy { get; set; }
        public virtual string? UpdatedBy { get; set; }
        public virtual EntityStatus Status { get; set; }
        public virtual DateTime CreatedAt { get; set; }
        public virtual DateTime UpdatedAt { get; set; }
    }
}
