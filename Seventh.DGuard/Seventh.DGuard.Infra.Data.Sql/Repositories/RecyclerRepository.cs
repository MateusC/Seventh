using Microsoft.EntityFrameworkCore;
using Seventh.DGuard.Domain.Entities;
using Seventh.DGuard.Domain.Repositories;
using System.Linq;
using System.Threading.Tasks;

namespace Seventh.DGuard.Infra.Data.Sql.Repositories
{
    public class RecyclerRepository : Repository<Recycler>, IRecyclerRepository
    {
        public RecyclerRepository(DGuardContext context) : base(context)
        {
        }

        public Task<Recycler> GetLastRun()
        {
            return _set.OrderByDescending(x => x.CreatedDate).FirstOrDefaultAsync();
        }
    }
}
