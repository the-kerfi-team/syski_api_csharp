using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace csharp.Data
{
    public class ApplicationUserSystems
    {

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public Guid SystemId { get; set; }

        [ForeignKey("SystemId")]
        public virtual System System { get; set; }

    }
}
