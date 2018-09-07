using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AspnetCoreLogging.Controllers
{
    public class TestController : Controller
    {
        private readonly ILogger _ilogger;
        public TestController(ILogger<TestController> logger)
        {
            _ilogger = logger;
        }
        public IActionResult Index()
        {
            _ilogger.LogInformation("Ankit test Controller??");
            return View("../Test/Contact");
        }
    }
}