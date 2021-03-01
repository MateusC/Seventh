using MediatR;
using Microsoft.Extensions.Logging;
using Seventh.DGuard.Application.Commands;
using Seventh.DGuard.Domain.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Seventh.DGuard.Application.CommandHandlers
{
    public class CheckServerStatusHandler : IRequestHandler<CheckServerStatusCommand, bool>
    {
        private readonly ILogger<CheckServerStatusHandler> _logger;
        private readonly IServerRepository _serverRepository;

        public CheckServerStatusHandler(IServerRepository serverRepository, ILogger<CheckServerStatusHandler> logger)
        {
            _logger = logger;
            _serverRepository = serverRepository;
        }

        public async Task<bool> Handle(CheckServerStatusCommand request, CancellationToken cancellationToken)
        {
            var server = await _serverRepository.GetAsync(request.ServerId);

            if (server is null)
            {
                _logger.LogInformation($"Não foi encontrado servidor com id {request.ServerId}.");

                return false;
            }

            _logger.LogInformation("Tentativa de chamar o servidor {0} com o endereço {1}:{2}", server.Id, server.IP, server.Port);

            try
            {
                return server.IsAvailable();
            }
            catch (Exception e)
            {
                _logger.LogError("Ocorreu um erro ao tentar chamar o servidor {0}. Erro: {1}. Stacktrace: {2}", server.Name, e.Message, e.StackTrace);

                return false;
            }
        }
    }
}