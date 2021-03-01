using MediatR;
using Microsoft.Extensions.Logging;
using Seventh.DGuard.Application.CommandHandlers.Responses;
using Seventh.DGuard.Application.Commands;
using Seventh.DGuard.Application.CommandValidators;
using Seventh.DGuard.Domain.Entities;
using Seventh.DGuard.Domain.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Seventh.DGuard.Application.CommandHandlers
{
    public class UpdateServerHandler : IRequestHandler<UpdateServerCommand, UpdateServerCommandResponse>
    {
        private readonly IServerRepository _serverRepository;
        private readonly UpdateServerCommandValidator _validator;
        private readonly ILogger<UpdateServerHandler> _logger;

        public UpdateServerHandler(UpdateServerCommandValidator validator, IServerRepository serverRepository, ILogger<UpdateServerHandler> logger)
        {
            _logger = logger;
            _serverRepository = serverRepository;
            _validator = validator;
        }

        public async Task<UpdateServerCommandResponse> Handle(UpdateServerCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors
                    .Select(x => x.ErrorMessage);

                return new UpdateServerCommandResponse(null, errors);
            }

            Server currentServer = await _serverRepository.GetAsync(request.Id);

            if(currentServer is null)
            {
                var errors = new List<string> { $"Servidor com identificador {request.Id} não encontrado." };

                return new UpdateServerCommandResponse(null, errors);
            }

            _logger.LogInformation("Atualizando servidor com id {0}", currentServer.Id);

            currentServer.ChangeInfo(request.Name, request.IP, request.Port);

            _serverRepository.Update(currentServer);

            await _serverRepository.SaveChangesAsync();

            _logger.LogInformation("Servidor com id {0} atualizado.", currentServer.Id);

            return new UpdateServerCommandResponse(currentServer, null);
        }
    }
}