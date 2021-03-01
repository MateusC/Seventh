using MediatR;
using Seventh.DGuard.Domain.Entities;
using System.Collections.Generic;

namespace Seventh.DGuard.Application.Queries
{
    public class GetServersQuery : IRequest<List<Server>>
    {
    }
}
