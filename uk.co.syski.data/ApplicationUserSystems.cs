using System;
using System.Collections.Generic;
using System.Text;

namespace Syski.Data
{
    public class ApplicationUserSystems
    {

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public Guid SystemId { get; set; }

        public System System { get; set; }

        public Guid? CategoryId { get; set; }

        public ApplicationUserSystemCategory Category { get; set; }

    }
}
