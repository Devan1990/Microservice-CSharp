using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Domain.Common;

namespace UserManagement.Domain.Entities
{
    public class Area : EntityBase
    {
        public string AreaName { get; set; }
        public Country Country { get; set; }
        public virtual ICollection<Region> Regions { get; set; }
    }
}
