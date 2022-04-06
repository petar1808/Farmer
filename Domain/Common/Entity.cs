using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common
{
    public abstract class Entity<TId> where TId : struct
    {
        protected Entity(TId id)
        {
            Id = id;
        }
        protected Entity()
        {
            Id = default!;
        }
        public TId Id { get;}
    }
}
