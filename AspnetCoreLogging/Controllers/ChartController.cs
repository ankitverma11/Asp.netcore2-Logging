using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using AspnetCoreLogging.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AspnetCoreLogging.Controllers
{
    public class ChartController : Controller
    {
        private readonly ILogger _logger;
        MenuModuleMappingContext _dbcontext;
        CoredataAccesslayers objaccess = new CoredataAccesslayers();
        public ChartController(ILogger<ChartController> logger , MenuModuleMappingContext dbcontext )
        {
            _logger = logger;
            _dbcontext = dbcontext;
        }
       
        public IActionResult BarChart()
        {
            var Leakagebardata = GetLeakage();
            return View(Leakagebardata);
        }

        //public IActionResult GetLeakageBarData()
        //{
        //   var Leakagebardata = GetLeakage();
        //   return Json(Leakagebardata);
        //}

        public IList<LeakageStatistics> GetLeakage()
        {
            List<LeakageStatistics> LeakageList = new List<LeakageStatistics>();
            DataTable dtleakageData = new DataTable();
            //var genacisContext = _dbcontext;
            //var dbconnection = genacisContext.Database.GetDbConnection();
            try
            {
                dtleakageData = objaccess.GetLeakageSystemView("System", Convert.ToDateTime("2018-08-01 00:00:00"), Convert.ToDateTime("2018-08-27 00:00:00"), 218, 0, 0, 195).Tables[0];
                LeakageList = (from DataRow dr in dtleakageData.Rows
                            select new LeakageStatistics()
                            {
                                LeakageRange = dr["LeakageRange"] != null ? dr["LeakageRange"].ToString() : "",
                                Occurences = dr["Occurences"] != null ? Convert.ToInt32(dr["Occurences"]) : 0,
                                LookUpId = dr["LookUpId"] != null ? Convert.ToInt32(dr["LookUpId"]) : 0,
                                LookUpKey = dr["LookUpKey"] != null ? dr["LookUpKey"].ToString() : "",
                                MTTR = dr["MTTR"] != null ? Convert.ToDecimal(dr["MTTR"].ToString()) : 0,
                            }).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString(), "ChartController");
            }
            return LeakageList;
        }
    }
}