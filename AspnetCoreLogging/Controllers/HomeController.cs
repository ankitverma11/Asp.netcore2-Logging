using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AspnetCoreLogging.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using System.Data;

namespace AspnetCoreLogging.Controllers
{
    //[Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger _logger;
        MenuModuleMappingContext _dbcontext;

        public HomeController(ILogger<HomeController> logger, MenuModuleMappingContext dbcontext)
        {
            _logger = logger;
            _dbcontext = dbcontext;
        }



        //private IConfiguration _configuration;  // Get data from App Setting
        //public HomeController(IConfiguration configuration)
        //{
        //    _configuration = configuration;
        //}

        [AllowAnonymous]  //To make an action method public in secure controller, we can mark them as Anonymous   action method will be accessible for all user even without logging in.
        //To pass error to the view we can use ModelState.AddModelError method (if the error is Model field specific) or simply ViewBag or ViewData can also be used.
        public IActionResult Index()
        {
            try
            {

                //if (configuration["LoggingSection:LoggingEnabled"] == "true")

                _logger.LogInformation("Ankit Index Method Called!!!");
                // _logger.LogCritical("AAA");


            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString(), "HomeController");
                _logger.LogInformation(ex.ToString());
            }
            return View();
        }

        public IActionResult About()
        {
            Serilog.Log.Logger.Information("info server log message");
            ViewData["Message"] = "Your application description page.";
            using (_logger.BeginScope("Log Scope Example"))
            {
                _logger.LogInformation("Begin Scope");
                _logger.LogWarning("Test Scope");
            }
            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        //public IActionResult CreateForValidation(model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        if (model.FirstName == null)
        //        {
        //            ViewBag.FirstNameError = "Please write first name";
        //        }
        //    }

        //    return View(model);
        //}

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> Image()
        {
            Image LeakImage = new Image();
            var genacisContext = _dbcontext;
            var dbConnection = genacisContext.Database.GetDbConnection();
            try
            {
                //SQL Parameters
                List<SqlParameter> sqlParamList = new List<SqlParameter>
                {
                    new SqlParameter("@ID",1509673),
                    new SqlParameter("@TypeOfID","Leak")
                };



                //Call stored procedure to get data
                using (var command = genacisContext.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = "GetLeakageImage";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddRange(sqlParamList.ToArray());
                    await dbConnection.OpenAsync();

                    using (var result = await command.ExecuteReaderAsync())
                    {
                        while (result.Read())
                        {

                            LeakImage.ImageData = result[0] as byte[];

                        }

                    }
                }


            }
            catch (Exception ex)
            {

            }
            finally
            {
                if (dbConnection.State == ConnectionState.Open)
                {
                    dbConnection.Close();
                }
            }
            // return File(ImageData, "image/jpg");
            return View(LeakImage);

        }


        public async Task<IActionResult> Download(string filename, string fileext) //string filename, string fileext
        {
            Image LeakImage = new Image();
            LeakImage.ImageData = null;
           // byte[] ImageData = null;
            var genacisContext = _dbcontext;
            var dbConnection = genacisContext.Database.GetDbConnection();
            try
            {
                //SQL Parameters
                List<SqlParameter> sqlParamList = new List<SqlParameter>
                {
                    new SqlParameter("@ID",1509673), //1509673
                    new SqlParameter("@TypeOfID","Leak")
                };



                //Call stored procedure to get data
                using (var command = genacisContext.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = "GetLeakageImage";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddRange(sqlParamList.ToArray());
                    await dbConnection.OpenAsync();

                    using (var result = await command.ExecuteReaderAsync())
                    {
                        while (result.Read())
                        {

                            LeakImage.ImageData = result[0] as byte[];

                        }

                    }
                }

            }
            finally
            {
                if (dbConnection.State == ConnectionState.Open)
                {
                    dbConnection.Close();
                }
            }
            //if (LeakImage.ImageData != null)
            //{
                return File(LeakImage.ImageData, "image/png", filename);
            //}
            //else
            //{
            //    ViewBag.Error = "Image not found or matched";
            //    return View("Image",LeakImage);
            //}
        }
    }
}

