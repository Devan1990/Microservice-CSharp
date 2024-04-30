using System;
using System.Collections.Generic;
using UserManagement.Domain.Common;

namespace UserManagement.Domain.Entities
{
    public class Role :EntityBase
    {
     
        public Role()
        {
            RoleGuid = Guid.NewGuid();
        }
        
        public Guid? RoleGuid { get; set; }
        public string RoleId { get; set; }      
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }
        public RoleType RoleType { get; set; }
        public bool IsDeleted { get; set; } = false;
       
        public virtual ICollection<CompetenciesMap> CompetenciesMap { get; set; }

    }
}
