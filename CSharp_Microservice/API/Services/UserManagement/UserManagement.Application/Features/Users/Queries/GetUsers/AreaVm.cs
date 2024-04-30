using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Application.Features.Users.Queries.GetUsers
{
    public class AreaVm
    {
        public int id { get; set; }

        public string AreaName { get; set; }
        public ICollection<RegionVm> Regions { get; set; }
    }
}
