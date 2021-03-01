using MediatR;
using Seventh.DGuard.Application.Queries;
using Seventh.DGuard.Domain.Repositories;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Seventh.DGuard.Application.QueryHandlers
{
    public class GetVideoContentHandler : IRequestHandler<GetVideoContentQuery, String>
    {
        private readonly IServerRepository _serverRepository;
        private readonly IVideoRepository _videoRepository;

        public GetVideoContentHandler(IServerRepository serverRepository, IVideoRepository videoRepository)
        {
            _serverRepository = serverRepository;
            _videoRepository = videoRepository;
        }

        public async Task<String> Handle(GetVideoContentQuery request, CancellationToken cancellationToken)
        {
            var server = await _serverRepository.GetAsync(request.ServerId);

            var video = server?.Videos?.FirstOrDefault(x => x.Id == request.VideoId);

            return _videoRepository.GetContent(video?.Path);
        }
    }
}
