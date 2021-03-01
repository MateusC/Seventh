using MediatR;
using Microsoft.Extensions.Logging;
using Seventh.DGuard.Application.CommandHandlers.Responses.Videos;
using Seventh.DGuard.Application.Commands.Videos;
using Seventh.DGuard.Domain.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Seventh.DGuard.Application.CommandHandlers.Videos
{
    public class CreateVideoHandler : IRequestHandler<CreateVideoCommand, CreateVideoCommandResponse>
    {
        private readonly ILogger<CreateVideoHandler> _logger;
        private readonly IServerRepository _serverRepository;
        private readonly IVideoRepository _videoRepository;

        public CreateVideoHandler(IServerRepository serverRepository, IVideoRepository videoRepository, ILogger<CreateVideoHandler> logger)
        {
            _logger = logger;
            _serverRepository = serverRepository;
            _videoRepository = videoRepository;
        }

        public async Task<CreateVideoCommandResponse> Handle(CreateVideoCommand request, CancellationToken cancellationToken)
        {
            var server = await _serverRepository.GetAsync(request.ServerId);

            if (server is null)
            {
                var message = $"Não foi encontrado servidor com id {request.ServerId}.";

                return FormatResponse(message, null);
            }

            try
            {
                String path = await _videoRepository.SaveContent(request.Content);

                var newVideo = server.AddVideo(path, request.Description);

                _serverRepository.Update(server);

                await _serverRepository.SaveChangesAsync();

                return new CreateVideoCommandResponse(newVideo, null);
            }
            catch (Exception ex)
            {
                var message = $"Não foi possível salvar o conteúdo do vídeo {request.Description}.";

                return FormatResponse(message, ex);
            }
        }

        private CreateVideoCommandResponse FormatResponse(string message, Exception ex)
        {
            if (ex is null)
                _logger.LogInformation(message);
            else
                _logger.LogError($"{message}. Erro: {ex.StackTrace}");

            return new CreateVideoCommandResponse(null, message);
        }
    }
}
