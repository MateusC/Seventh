using MediatR;
using Seventh.DGuard.Application.CommandHandlers.Responses;
using System;

namespace Seventh.DGuard.Application.Commands
{
    public class UpdateServerCommand : IRequest<UpdateServerCommandResponse>
    {
        public UpdateServerCommand()
        {

        }

        public UpdateServerCommand(Guid id, string name, string iP, ushort port)
        {
            Id = id;
            Name = name;
            IP = iP;
            Port = port;
        }

        public Guid Id { get; set; }

        public String Name { get; set; }

        public String IP { get; set; }

        public UInt16 Port { get; set; }
    }
}
