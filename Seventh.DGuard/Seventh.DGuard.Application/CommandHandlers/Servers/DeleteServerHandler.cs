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
    public class DeleteServerHandler : IRequestHandler<DeleteServerCommand, DeleteServerCommandResponse>
    {
        private readonly IServerRepository _serverRepository;
        private readonly ILogger<DeleteServerHandler> _logger;

        public DeleteServerHandler(IServerRepository serverRepository, ILogger<DeleteServerHandler> logger)
        {
            _logger = logger;
            _serverRepository = serverRepository;
        }

        public async Task<DeleteServerCommandResponse> Handle(DeleteServerCommand request, CancellationToken cancellationToken)
        {
            if (request.ServerId == Guid.Empty)
            {
                return new DeleteServerCommandResponse(false, "Identificador do servidor é obrigatório.");
            }

            var result = _serverRepository.Delete(request.ServerId);

            await _serverRepository.SaveChangesAsync();

            if (result)
            {
                _logger.LogInformation("Servidor {0} excluído.", request.ServerId);

                return new DeleteServerCommandResponse(result, null);
            }

            return new DeleteServerCommandResponse(false, $"Não foi encontrado servidor com identificador {request.ServerId}");
        }
    }
}