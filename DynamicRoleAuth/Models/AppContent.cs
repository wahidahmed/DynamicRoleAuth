using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DynamicRoleAuth.Models
{
    public class AppContent
    {
        public int ID { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string Description { get; set; }
    }
}