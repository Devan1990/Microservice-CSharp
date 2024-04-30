using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Domain.Common;

namespace UserManagement.Domain.Entities
{
    public class AssessorRole : EntityBase
    {

        public bool Mandatory { get; set; }
        public long RoleId { get; set; }
        public bool IsDeleted { get; set; } = false;
        public RoleMapping roleMapping { get; set; }

    }
}
