using System;

namespace AE.HealthSystem.Domain.Entities
{
    public abstract class BaseEntity<T>
    {
        public long Id { get; protected set; }

    }
}
