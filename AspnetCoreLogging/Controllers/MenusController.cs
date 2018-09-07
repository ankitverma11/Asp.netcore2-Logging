using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AspnetCoreLogging.Models;
using Microsoft.Extensions.Logging;
using AspnetCoreLogging.Service;
using System.Text;
using System.Runtime.Serialization.Json;

namespace AspnetCoreLogging.Controllers
{
    public class MenusController : Controller
    {
       // private readonly MenuContext _context;
        private readonly ILogger _ilogger;
        MenuModuleMapping_CoreContext _context;
        MenumoduleMappingService _iservice;
        MenuModuleMapping_CoreContext _CContext;
     

        public MenusController(ILogger<MenusController> logger, MenuModuleMapping_CoreContext context , MenumoduleMappingService iservice ,MenuModuleMapping_CoreContext coreContext)
        {
            _ilogger = logger;
            _context = context;
            _iservice = iservice;
            _CContext = coreContext;
        }



        // GET: Menus
        public IActionResult Index()
        {
             var menudata = _iservice.GetMenuByUser();
            ViewBag.MenuData = menudata;
            //var menudata = _iservice.GetMenuModuleMapping();
            return View(menudata);
        }

        public IActionResult Edit()
        {
            //var menudata = _iservice.GetMenuByUser();
            //ViewBag.MenuData = menudata;
            //return View(menudata);
            List<MenuModuleMapping_Core> parentList = new List<MenuModuleMapping_Core>();
            List<MenuModuleMapping_Core> SubmenuList = new List<MenuModuleMapping_Core>();
            List<MenuModuleMapping_Core> ChildMenuList = new List<MenuModuleMapping_Core>();

            parentList = _CContext.MenuModuleMapping_Core.Where(s => s.ParentMenuID == 0).ToList();
            SubmenuList = _CContext.MenuModuleMapping_Core.Where(s => s.ParentMenuID == 1 && s.ModuleID != null).ToList();
            ChildMenuList = _CContext.MenuModuleMapping_Core.Where(s => s.ParentMenuID == 2 && s.ModuleID != null).ToList();

            MainModel mainModel = new MainModel();
            mainModel.ParentList = parentList;
            mainModel.SubMenuList = SubmenuList;
            mainModel.ChildMenuList = ChildMenuList;

            return View(mainModel);
        }

        public   IActionResult Sidemenu()
        {
            return PartialView();
        }

        public ActionResult MenuListData()
        {
            List<MenuModuleMapping_Core> parentList = new List<MenuModuleMapping_Core>();
            List<MenuModuleMapping_Core> SubmenuList = new List<MenuModuleMapping_Core>();
            List<MenuModuleMapping_Core> ChildMenuList = new List<MenuModuleMapping_Core>();

            parentList = _CContext.MenuModuleMapping_Core.Where(s => s.ParentMenuID == 0).ToList();
            SubmenuList = _CContext.MenuModuleMapping_Core.Where(s => s.ParentMenuID == 1 && s.ModuleID != null).ToList();
            ChildMenuList = _CContext.MenuModuleMapping_Core.Where(s => s.ParentMenuID == 2 && s.ModuleID != null).ToList();

            MainModel mainModel = new MainModel();
            mainModel.ParentList = parentList;
            mainModel.SubMenuList = SubmenuList;
            mainModel.ChildMenuList = ChildMenuList;

           return View(mainModel);
        }





        public ViewResult MenuData()
        {
            //var Listdata = TempData["MenuData"];
            //var IData = Listdata.(s => s.ParentMenuID == 1 && s.ModuleID != null).ToList();
            return View();
        }

        public JsonResult jsonResult()
        {
            var menudata = _iservice.GetMenuByUser();
            return Json(menudata);
            //var jsonData = Json(menudata);
            //ViewBag.MenuData = jsonData;
            //return jsonData;

        }

        public IActionResult Parent(int id)
        {
            var menudata = _iservice.GetMenuByUser();
            ViewBag.MenuData = menudata;
            //var menudata = _iservice.GetMenuModuleMapping();
            return View(menudata);
        }

        public IActionResult Details()
        {
            return View();
        }

        public JsonResult MenuDataAsJSON()
        {
            var MenuJsonList = _iservice.GetMenuByUser();
            return Json(MenuJsonList);
        }

        public ActionResult SubMenuDetails(int MenuID)
        {
            object model = null;
            model = _iservice.GetMenuByUser().Where(s => s.ParentMenuID == 1 && s.ModuleID != null).ToList();
            return PartialView("~\\Views\\Shared\\_SubMenu.cshtml", model);
        }

       

        public ActionResult MenuList()
        {
            //IList<MenuModuleMapping_Core> MenuL = new List<MenuModuleMapping_Core>();

            //List<MenuModuleMapping_Core> menu = _iservice.GetMenuByUser().Select(e => new MenuModuleMapping_Core
            //{
            //    ParentMenuID = e.ParentMenuID,
            //    MenuID = e.MenuID,
            //    ChildMenu = GetChildren(_iservice.GetMenuByUser().ToList(), e.ParentMenuID)
            //}).ToList();
            //ViewBag.menusList = menu;
            //return View();
            var modal = _iservice.GetMenuByUser();
            return View(modal);

        }



        /// <summary>
        /// Recursively grabs the children from the list of items for the provided parentId
        /// </summary>
        /// <param name="items">List of all items</param>
        /// <param name="parentId">Id of parent item</param>
        /// <returns>List of children of parentId</returns>
        //private static List<MenuModuleMapping_Core> GetChildren(List<MenuModuleMapping_Core> items, int parentId)
        //{
        //    return items
        //        .Where(x => x.ParentMenuID == parentId)
        //        .Select(e => new MenuModuleMapping_Core
        //        {
        //            MenuName = e.MenuName,
        //            ParentMenuID = e.ParentMenuID,
        //            ChildMenu = GetChildren(items, e.ParentMenuID)
        //        }).ToList();
        //}

    }
}
