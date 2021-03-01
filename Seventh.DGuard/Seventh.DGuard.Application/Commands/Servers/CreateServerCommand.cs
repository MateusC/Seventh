using MediatR;
using Seventh.DGuard.Application.CommandHandlers.Responses;
using System;

namespace Seventh.DGuard.Application.Commands
{
    public class CreateServerCommand : IRequest<CreateServerCommandResponse>
    {
        protected CreateServerCommand()
        {
        }

        public CreateServerCommand(string name, string iP, ushort port)
        {
            Name = name;
            IP = iP;
            Port = port;
        }

        public String Name { get; set; }

        public String IP { get; set; }

        public UInt16 Port { get; set; }
    }
}
