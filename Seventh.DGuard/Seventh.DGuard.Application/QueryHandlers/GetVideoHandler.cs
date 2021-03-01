using MediatR;
using Seventh.DGuard.Application.Queries;
using Seventh.DGuard.Domain.Entities;
using Seventh.DGuard.Domain.Repositories;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Seventh.DGuard.Application.QueryHandlers
{
    public class GetVideoHandler : IRequestHandler<GetVideoQuery, Video>
    {
        private readonly IServerRepository _serverRepository;

        public GetVideoHandler(IServerRepository serverRepository)
        {
            _serverRepository = serverRepository;
        }

        public async Task<Video> Handle(GetVideoQuery request, CancellationToken cancellationToken)
        {
            var server = await _serverRepository.GetAsync(request.ServerId);

            return server?.Videos?.FirstOrDefault(x => x.Id == request.VideoId);
        }
    }
}
