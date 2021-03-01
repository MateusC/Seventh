using Seventh.DGuard.Domain.Core;
using Seventh.DGuard.Domain.Entities;
using System.Threading.Tasks;

namespace Seventh.DGuard.Domain.Repositories
{
    public interface IRecyclerRepository : IRepository<Recycler>
    {
        Task<Recycler> GetLastRun();
    }
}
