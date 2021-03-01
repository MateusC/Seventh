using System;

namespace Seventh.DGuard.Domain.Enums
{
    public class JobStatus
    {
        public static readonly JobStatus NotRunning = new JobStatus(1, "not running");
        public static readonly JobStatus Running = new JobStatus(2, "running");
        public static readonly JobStatus Executed = new JobStatus(3, "executed");

        protected JobStatus(Int32 id, String description)
        {
            Id = id;
            Description = description;
        }

        public Int32 Id { get; }

        public String Description { get; }

    }
}
