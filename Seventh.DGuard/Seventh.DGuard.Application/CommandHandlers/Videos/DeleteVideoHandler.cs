using MediatR;
using Microsoft.Extensions.Logging;
using Seventh.DGuard.Application.CommandHandlers.Responses.Videos;
using Seventh.DGuard.Application.Commands.Videos;
using Seventh.DGuard.Application.Exceptions;
using Seventh.DGuard.Domain.Repositories;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Seventh.DGuard.Application.CommandHandlers.Videos
{
    public class DeleteVideoHandler : IRequestHandler<DeleteVideoCommand, DeleteVideoCommandResponse>
    {
        private readonly ILogger<DeleteVideoHandler> _logger;
        private readonly IServerRepository _serverRepository;
        private readonly IVideoRepository _videoRepository;

        public DeleteVideoHandler(IServerRepository serverRepository, IVideoRepository videoRepository, ILogger<DeleteVideoHandler> logger)
        {
            _logger = logger;
            _serverRepository = serverRepository;
            _videoRepository = videoRepository;
        }

        public async Task<DeleteVideoCommandResponse> Handle(DeleteVideoCommand request, CancellationToken cancellationToken)
        {
            var server = await _serverRepository.GetAsync(request.ServerId);

            if (server is null)
            {
                _logger.LogInformation($"Não foi encontrado servidor com id {request.ServerId}.");

                return new DeleteVideoCommandResponse(false, new ServerNotFoundException(request.ServerId));
            }

            var currentVideo = server.Videos.FirstOrDefault(x => x.Id == request.VideoId);

            if(currentVideo is null)
            {
                _logger.LogInformation($"Não foi encontrado video com id {request.ServerId}.");

                return new DeleteVideoCommandResponse(false, new VideoNotFoundException(request.VideoId));
            }

            await _videoRepository.RemoveContent(currentVideo.Path);

            _serverRepository.RemoveVideo(currentVideo);

            await _serverRepository.SaveChangesAsync();

            return new DeleteVideoCommandResponse(true, null);
        }
    }
}