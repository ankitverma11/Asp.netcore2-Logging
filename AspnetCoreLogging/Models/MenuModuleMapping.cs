using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspnetCoreLogging.Models
{
    public partial class MenuModuleMapping
    {
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
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
