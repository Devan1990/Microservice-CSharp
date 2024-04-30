using CompetencyFramework.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Domain.Entities
{
    public class CompetenciesMap : EntityBase
    {
        public int CompetencyGroupId { get; set; }
        public int CompetencyId { get; set; }
        public bool IsSelected { get; set; } = true;
        public long ExpectedLevelId { get; set; }
        public bool IsDeleted { get; set; } = false;


    }
}
