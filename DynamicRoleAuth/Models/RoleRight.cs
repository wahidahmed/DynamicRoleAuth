using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DynamicRoleAuth.Models
{
    public class RoleRight
    {
        public int ID { get; set; }
        public string RoleName { get; set; }
        public int AppContentId { get; set; }
        public AppContent AppContent { get; set; }
    }
}