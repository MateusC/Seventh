using MediatR;
using Seventh.DGuard.Application.CommandHandlers.Responses.Videos;
using System;

namespace Seventh.DGuard.Application.Commands.Videos
{
    public class DeleteVideoCommand : IRequest<DeleteVideoCommandResponse>
    {
        public DeleteVideoCommand()
        {

        }

        public DeleteVideoCommand(Guid serverId, Guid videoId)
        {
            ServerId = serverId;
            VideoId = videoId;
        }

        public Guid ServerId { get; set; }

        public Guid VideoId { get; set; }
    }
}
