
using UserManagement.Domain.Common;

namespace UserManagement.Domain.Entities
{
    public class User : EntityBase
    {
       public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Country Country { get; set; }
        public Area Area { get; set; }
        public Region Region { get; set; }
        public Role Role { get; set; }
        public Verticals Vertical { get; set; }
        public ActiveStatus Status { get; set; }
        public bool IsDeleted { get; set; } = false;


    }
}
