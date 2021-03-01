using MediatR;
using Seventh.DGuard.Application.CommandHandlers.Responses;
using System;

namespace Seventh.DGuard.Application.Commands
{
    public class DeleteServerCommand : IRequest<DeleteServerCommandResponse>
    {
        protected DeleteServerCommand()
        {

        }

        public DeleteServerCommand(Guid serverId)
        {
            ServerId = serverId;
        }

        public Guid ServerId { get; set; }
    }
}
