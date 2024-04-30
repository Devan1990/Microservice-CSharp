using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Domain.Common;

namespace UserManagement.Domain.Entities
{
    public class Country : EntityBase
    {

        public string CountryName { get; set; }
        public ICollection<Area> Areas { get; set; }
  


    }
}
