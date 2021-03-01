using MediatR;
using Seventh.DGuard.Application.Queries;
using Seventh.DGuard.Domain.Enums;
using Seventh.DGuard.Domain.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace Seventh.DGuard.Application.QueryHandlers
{
    public sealed class GetRecyclerStatusHandler : IRequestHandler<GetRecyclerStatusQuery, JobStatus>
    {
        private readonly IRecyclerRepository _recyclerRepository;

        public GetRecyclerStatusHandler(IRecyclerRepository recyclerRepository)
        {
            _recyclerRepository = recyclerRepository;
        }

        public async Task<JobStatus> Handle(GetRecyclerStatusQuery request, CancellationToken cancellationToken)
        {
            var lastRun = await _recyclerRepository.GetLastRun();

            if (lastRun is null)
                return JobStatus.NotRunning;

            return lastRun.IsRunning ? JobStatus.Running : JobStatus.NotRunning;
        }
    }
}
