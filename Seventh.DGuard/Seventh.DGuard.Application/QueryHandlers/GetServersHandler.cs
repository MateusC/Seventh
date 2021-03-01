using MediatR;
using Seventh.DGuard.Domain.Entities;
using Seventh.DGuard.Domain.Repositories;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Seventh.DGuard.Application.Queries
{
    public class GetServersHandler : IRequestHandler<GetServersQuery, List<Server>>
    {
        private readonly IServerRepository _serverRepository;

        public GetServersHandler(IServerRepository serverRepository)
        {
            _serverRepository = serverRepository;
        }

        public Task<List<Server>> Handle(GetServersQuery request, CancellationToken cancellationToken)
        {
            return _serverRepository.GetAll(false);
        }
    }
}
