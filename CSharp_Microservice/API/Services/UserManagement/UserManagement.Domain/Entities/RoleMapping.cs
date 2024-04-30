using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Domain.Common;

namespace UserManagement.Domain.Entities
{
    public class RoleMapping : EntityBase
    {
        public RoleMapping()
        {
            AssessorRole = new List<AssessorRole>();
        }
        public DateTime AssessmentPeriodFrom { get; set; }
        public DateTime AssessmentPeriodTo { get; set; }
        public long RoleId { get; set; }

        public bool IsDeleted { get; set; } = false;

        public virtual ICollection<AssessorRole> AssessorRole { get; set; }
    }
}
