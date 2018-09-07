using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspnetCoreLogging.Models
{
    public class ChildMenu
    {
        public int MenuId { get; set; }
        public string MenuName { get; set; }
        public int? ParentMenuId { get; set; }
        public string MenuPage { get; set; }
        public int? ModuleId { get; set; }
        public int? ModuleFunction { get; set; }
        public int? ParentMenuOrder { get; set; }
        public int? MenuOrder { get; set; }
        public bool? IsForSmartPhoneApp { get; set; }
    }
}
