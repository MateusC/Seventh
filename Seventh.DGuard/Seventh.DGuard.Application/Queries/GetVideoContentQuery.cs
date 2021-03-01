using MediatR;
using System;

namespace Seventh.DGuard.Application.Queries
{
    public class GetVideoContentQuery : IRequest<String>
    {
        protected GetVideoContentQuery()
        {
        }

        public GetVideoContentQuery(Guid serverId, Guid videoId)
        {
            ServerId = serverId;
            VideoId = videoId;
        }

        public Guid ServerId { get; set; }

        public Guid VideoId { get; set; }
    }
}
