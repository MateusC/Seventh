using System;

namespace Seventh.DGuard.Domain.Core
{
    public class Entity : IEquatable<Entity>
    {
        public Entity()
        {
            Id = Guid.NewGuid();
        }

        public Entity(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; protected set; }

        public override bool Equals(object obj)
        {
            if (obj is null)
                return false;

            if (obj.GetType() != GetType())
                return false;

            if (((Entity)obj).Id == Id)
                return true;

            return false;
        }

        public bool Equals(Entity other)
        {
            return 
                other != null &&
                Id.Equals(other.Id);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
