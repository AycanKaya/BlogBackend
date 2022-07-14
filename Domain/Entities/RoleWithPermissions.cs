using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class RoleWithPermissions
    {
        [ForeignKey("AspNetRoles")]
        public string RoleId { get; set; } 
        public string PermissionId { get; set; }

    }
}
