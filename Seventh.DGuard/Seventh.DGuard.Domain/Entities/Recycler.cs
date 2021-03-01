using Seventh.DGuard.Domain.Core;
using Seventh.DGuard.Domain.Enums;
using Seventh.DGuard.Domain.Util;
using System;

namespace Seventh.DGuard.Domain.Entities
{
    public class Recycler : Entity
    {
        public Recycler()
        {
            CreatedDate = Clock.Now;
            Status = JobStatus.Running;
        }

        public DateTime CreatedDate { get; private set; }

        public DateTime? UpdatedDate { get; private set; }

        public JobStatus Status { get; private set; }

        public bool IsRunning => UpdatedDate is null;

        public void Executed()
        {
            UpdatedDate = Clock.Now;
            Status = JobStatus.Executed;
        }
    }
}
