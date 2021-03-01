using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Seventh.DGuard.API.Models
{
    public sealed class RecyclerStatusViewModel
    {
        public RecyclerStatusViewModel()
        {

        }

        public RecyclerStatusViewModel(string status)
        {
            Status = status;
        }

        public string Status { get; set; }
    }
}
