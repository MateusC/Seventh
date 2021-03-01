using MediatR;
using Microsoft.Extensions.Logging;
using Seventh.DGuard.Application.CommandHandlers.Responses.Videos;
using Seventh.DGuard.Application.Commands.Videos;
using Seventh.DGuard.Domain.Repositories;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Seventh.DGuard.Application.CommandHandlers.Videos
{
    public class RecoverVideoHandler : IRequestHandler<RecoverVideoCommand, RecoverVideoCommandResponse>
    {
        private readonly ILogger<RecoverVideoHandler> _logger;
        private readonly IServerRepository _serverRepository;

        public RecoverVideoHandler(IServerRepository serverRepository, ILogger<RecoverVideoHandler> logger)
        {
            _logger = logger;
            _serverRepository = serverRepository;
        }

        public async Task<RecoverVideoCommandResponse> Handle(RecoverVideoCommand request, CancellationToken cancellationToken)
        {
            var server = await _serverRepository.GetAsync(request.ServerId, true);

            if (server is null)
            {
                return FormatResponse($"Não foi encontrado servidor com id {request.ServerId}.");
            }

            var currentVideo = server.Videos.FirstOrDefault(x => x.Id == request.VideoId);

            if (currentVideo is null)
            {
                return FormatResponse($"Não foi encontrado video com id {request.VideoId}.");
            }

            //TODO: recuperar dados de arquivo não é possível após exclusão, teria que mover para uma "lixeira lógica" por exemplo

            _serverRepository.RecoverVideo(currentVideo);

            await _serverRepository.SaveChangesAsync();

            return new RecoverVideoCommandResponse(true, null);
        }

        private RecoverVideoCommandResponse FormatResponse(string message)
        {
            _logger.LogInformation(message);

            return new RecoverVideoCommandResponse(false, message);
        }
    }
}
