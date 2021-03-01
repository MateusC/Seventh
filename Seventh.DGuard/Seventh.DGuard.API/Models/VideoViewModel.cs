using System;

namespace Seventh.DGuard.API.Models
{
    public class VideoViewModel
    {
        protected VideoViewModel()
        {

        }

        public VideoViewModel(Guid id, string description)
        {
            Id = id;
            Description = description;
        }

        public Guid Id { get; set; }

        public String Description { get; set; }
    }
}
