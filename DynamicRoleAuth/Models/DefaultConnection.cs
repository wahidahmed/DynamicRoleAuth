using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DynamicRoleAuth.Models
{
    public class DefaultConnection:DbContext
    {
        public DbSet<AppContent> AppContents { get; set; }
        public DbSet<RoleRight> RoleRights { get; set; }
    }
}