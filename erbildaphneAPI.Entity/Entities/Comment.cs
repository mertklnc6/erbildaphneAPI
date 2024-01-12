using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace erbildaphneAPI.Entity.Entities
{
    public class Comment:BaseEntity
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public bool IsVerified { get; set; } = false;
        
    }
}
