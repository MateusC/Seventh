using MediatR;
using Seventh.DGuard.Application.CommandHandlers.Responses;
using System;

namespace Seventh.DGuard.Application.Commands
{
    public class RecoverServerCommand : IRequest<RecoverServerCommandResponse>
    {
        protected RecoverServerCommand()
        {

        }

        public RecoverServerCommand(Guid serverId)
        {
            ServerId = serverId;
        }

        public Guid ServerId { get; set; }
    }
}
