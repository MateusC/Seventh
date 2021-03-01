using MediatR;
using Microsoft.Extensions.Logging;
using Seventh.DGuard.Application.CommandHandlers.Responses;
using Seventh.DGuard.Application.Commands;
using Seventh.DGuard.Domain.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Seventh.DGuard.Application.CommandHandlers
{
    public class RecoverServerHandler : IRequestHandler<RecoverServerCommand, RecoverServerCommandResponse>
    {
        private readonly IServerRepository _serverRepository;
        private readonly ILogger<RecoverServerHandler> _logger;

        public RecoverServerHandler(IServerRepository serverRepository, ILogger<RecoverServerHandler> logger)
        {
            _logger = logger;
            _serverRepository = serverRepository;
        }

        public async Task<RecoverServerCommandResponse> Handle(RecoverServerCommand request, CancellationToken cancellationToken)
        {
            if (request.ServerId == Guid.Empty)
            {
                return new RecoverServerCommandResponse(false, "Identificador do servidor é obrigatório.");
            }

            var result = await _serverRepository.RecoverAsync(request.ServerId);

            await _serverRepository.SaveChangesAsync();

            if (result)
            {
                _logger.LogInformation("Servidor {0} recuperado.", request.ServerId);

                return new RecoverServerCommandResponse(result, null);
            }

            return new RecoverServerCommandResponse(false, $"Não foi encontrado servidor com identificador {request.ServerId}");
        }
    }
}