using Seventh.DGuard.Domain.Core;
using Seventh.DGuard.Domain.Util;
using System;

namespace Seventh.DGuard.Domain.Entities
{
    public class Video : Entity
    {
        protected Video()
        {

        }

        public Video(String description, String path)
        {
            Description = description;
            Path = path;
            CreatedDate = Clock.Now;
        }

        public DateTime CreatedDate { get; private set; }

        public String Description { get; private set; }

        public String Path { get; private set; }
    }
}
