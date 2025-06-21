using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Data
{
    public abstract class GenericLocalRepository<R,MC,Model>
    where R : class , IGenericRepository<Model>
    where Model : Aggregate<Guid>
    {
        public R repository;
        protected GenericLocalRepository(R repository)
        {
            this.repository = repository;
        }


        public R getMasterRepository()
        {
            return repository;
        }
    }
}
