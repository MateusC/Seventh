using MediatR;
using Seventh.DGuard.Application.CommandHandlers.Responses.Videos;
using System;

namespace Seventh.DGuard.Application.Commands.Videos
{
    public class CreateVideoCommand : IRequest<CreateVideoCommandResponse>
    {
        protected CreateVideoCommand()
        {

        }

        public CreateVideoCommand(Guid serverId, String description, String content)
        {
            Description = description;
            Content = content;
            ServerId = serverId;
        }

        public String Description { get; set; }

        public String Content { get; set; }

        public Guid ServerId { get; set; }
    }
}
