using MediatR;
using Microsoft.Extensions.Logging;
using Seventh.DGuard.Application.CommandHandlers.Responses;
using Seventh.DGuard.Application.Commands;
using Seventh.DGuard.Application.CommandValidators;
using Seventh.DGuard.Domain.Entities;
using Seventh.DGuard.Domain.Repositories;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Seventh.DGuard.Application.CommandHandlers
{
    public class CreateServerHandler : IRequestHandler<CreateServerCommand, CreateServerCommandResponse>
    {
        private readonly IServerRepository _serverRepository;
        private readonly CreateServerCommandValidator _validator;
        private readonly ILogger<CreateServerHandler> _logger;

        public CreateServerHandler(CreateServerCommandValidator validator, IServerRepository serverRepository, ILogger<CreateServerHandler> logger)
        {
            _logger = logger;
            _serverRepository = serverRepository;
            _validator = validator;
        }

        public async Task<CreateServerCommandResponse> Handle(CreateServerCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors
                    .Select(x => x.ErrorMessage);

                return new CreateServerCommandResponse(null, errors);
            }

            _logger.LogInformation("Adicionando novo servidor {0}", request.Name);

            var newServer = new Server(request.Name, request.IP, request.Port);

            var createdServer = await _serverRepository.AddAsync(newServer);

            await _serverRepository.SaveChangesAsync();

            _logger.LogInformation("Servidor {0} criado.", createdServer.Name);

            return new CreateServerCommandResponse(createdServer, null);
        }
    }
}