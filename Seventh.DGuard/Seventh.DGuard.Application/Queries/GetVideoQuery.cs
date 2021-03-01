using MediatR;
using Seventh.DGuard.Domain.Entities;
using System;

namespace Seventh.DGuard.Application.Queries
{
    public class GetVideoQuery : IRequest<Video>
    {
        protected GetVideoQuery()
        {

        }

        public GetVideoQuery(Guid serverId, Guid videoId)
        {
            ServerId = serverId;
            VideoId = videoId;
        }

        public Guid ServerId { get; set; }

        public Guid VideoId { get; set; }
    }
}
