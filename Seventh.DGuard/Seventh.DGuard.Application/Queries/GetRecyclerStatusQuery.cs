using MediatR;
using Seventh.DGuard.Domain.Enums;

namespace Seventh.DGuard.Application.Queries
{
    public class GetRecyclerStatusQuery : IRequest<JobStatus>
    {
    }
}
