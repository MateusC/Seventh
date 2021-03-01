using MediatR;
using System;

namespace Seventh.DGuard.Application.Commands
{
    public class CheckServerStatusCommand : IRequest<bool>
    {
        protected CheckServerStatusCommand()
        {

        }

        public CheckServerStatusCommand(Guid serverId)
        {
            ServerId = serverId;
        }

        public Guid ServerId { get; set; }
    }
}
