using System.Collections.Generic;

namespace UserManagement.Application.Features.Users.Queries.GetUsers
{
    public class CountryVm
    {
        public int id { get; set; }
        public string CountryName { get; set; }
        public ICollection<AreaVm> Areas { get; set; }
    }
}
