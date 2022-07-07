using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class RoleWithPermissions
    {
        public int RoleId { get; set; } //IdentityRole foreignkey
        public int PermissionId { get; set; }

    }
}
