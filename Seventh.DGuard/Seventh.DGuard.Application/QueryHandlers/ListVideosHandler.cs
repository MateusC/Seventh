using MediatR;
using Seventh.DGuard.Application.Queries;
using Seventh.DGuard.Domain.Entities;
using Seventh.DGuard.Domain.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Seventh.DGuard.Application.QueryHandlers
{
    public class ListVideosHandler : IRequestHandler<ListVideosQuery, List<Video>>
    {
        private readonly IServerRepository _serverRepository;

        public ListVideosHandler(IServerRepository serverRepository)
        {
            _serverRepository = serverRepository;
        }

        public async Task<List<Video>> Handle(ListVideosQuery request, CancellationToken cancellationToken)
        {
            var server = await _serverRepository.GetAsync(request.ServerId);

            if (server is null || server.Videos is null)
                return new List<Video>(0);

            return server.Videos.ToList();
        }
    }
}
