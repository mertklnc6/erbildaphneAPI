using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace erbildaphneAPI.Entity.DTOs
{
    public class EditRoleDto
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public string[] UserIdsToAdd { get; set; }
        public string[] UserIdsToDelete { get; set; }
    }
}
