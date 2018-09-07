using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspnetCoreLogging.Models
{
    public class MainModel
    {
       
        public List<MenuModuleMapping_Core> ParentList { get; set; }
        public List<MenuModuleMapping_Core> SubMenuList { get; set; }
        public List<MenuModuleMapping_Core> ChildMenuList { get; set;}
    }
}
