using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspnetCoreLogging.Models;


namespace AspnetCoreLogging.Service
{
    public class MenumoduleMappingService : IMenuModuleMappingService
    {
        private readonly MenuModuleMappingContext _dbContext;
        private readonly MenuModuleMapping_CoreContext _CoreContext;

        public MenumoduleMappingService(MenuModuleMappingContext dbcontext, MenuModuleMapping_CoreContext coreContext)
        {
            _dbContext = dbcontext;
            _CoreContext = coreContext;
        }


        public IEnumerable<MenuModuleMapping> GetMenuModuleMapping()
        {
            return _dbContext.MenuModuleMapping.Where(s => s.ParentMenuId == 0).ToList();
        }

        public IEnumerable<MenuModuleMapping> GetSubmenu(int? ParentMenuId)
        {
            return _dbContext.MenuModuleMapping.Where(s => s.ParentMenuId == ParentMenuId && s.ModuleId != null).ToList(); //ParentMenuId
        }

        public IEnumerable<MenuModuleMapping> GetChildMenuOfSubMenu(int? MenuID)
        {
            return _dbContext.MenuModuleMapping.Where(s => s.ParentMenuId == MenuID && s.ModuleId != null).ToList();
        }

        public IEnumerable<MenuModuleMapping_Core> GetMenuByUser()
        {
            return _CoreContext.MenuModuleMapping_Core.ToList();
        }

        public IEnumerable<MenuModuleMapping_Core> GetMenuByparent(int pmenuID)
        {
            return _CoreContext.MenuModuleMapping_Core.Where(m => m.ParentMenuID == pmenuID).ToList();
        }
    }
}
