using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Domain.Common;

namespace UserManagement.Domain.Entities
{
    public class Region: EntityBase
    {    
        public string RegionName { get; set; }       
        public Area Area { get; set; }

    }
}
