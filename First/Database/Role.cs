using System;
using System.Collections.Generic;

namespace First.Database
{
    public partial class Role
    {
        public Role()
        {
            Users = new HashSet<User>();
        }

        public Guid RoleId { get; set; }
        public string RoleName { get; set; } = null!;
        public bool IsActive { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
