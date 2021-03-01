using MediatR;
using Seventh.DGuard.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Seventh.DGuard.Application.Queries
{
    public class ListVideosQuery : IRequest<List<Video>>
    {
        public ListVideosQuery()
        {

        }

        public ListVideosQuery(Guid serverId)
        {
            ServerId = serverId;
        }

        public Guid ServerId { get; set; }
    }
}
