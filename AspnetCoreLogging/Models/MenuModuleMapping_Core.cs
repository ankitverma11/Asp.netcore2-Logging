using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AspnetCoreLogging.Models
{
    public class MenuModuleMapping_Core
    {
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int MenuID { get; set; }
        public string MenuName { get; set; }
        public int ParentMenuID { get; set; }
        public string MenuPage { get; set;}
        public int? ParentMenuOrder { get; set; } 
        public int? ModuleID { get; set; }
        public int? ModuleFunction { get; set; }
        public bool? IsSubscribable { get; set; }
        public int? MenuOrder { get; set; }
        //public virtual List<MenuModuleMapping_Core> ChildMenu { get; set; }
    }
}
