using MediatR;
using System;

namespace Seventh.DGuard.Application.Commands.Recyclers
{
    public class RecyclerStartCommand : IRequest
    {
        protected RecyclerStartCommand()
        {
        }

        public RecyclerStartCommand(UInt32 days)
        {
            Days = days;
        }

        public UInt32 Days { get; private set; }
    }
}
