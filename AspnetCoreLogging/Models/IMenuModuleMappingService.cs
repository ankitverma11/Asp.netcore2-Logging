using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspnetCoreLogging.Models
{
    public interface IMenuModuleMappingService
    {
        IEnumerable<MenuModuleMapping> GetMenuModuleMapping();

        IEnumerable<MenuModuleMapping> GetSubmenu(int? ParentMenuId);

        IEnumerable<MenuModuleMapping> GetChildMenuOfSubMenu(int? ParentMenuId);

        IEnumerable<MenuModuleMapping_Core> GetMenuByUser();

        IEnumerable<MenuModuleMapping_Core> GetMenuByparent(int pmenuID);
    }
}
