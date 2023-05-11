using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace CoronaManagementSystemHMO.Controllers
{
    [Route("")]
    public class InitController : Controller
    {
        [HttpGet("")]
        public IActionResult DefaultEntryPoint()
        {
            return Content("Service started...");
        }
    }
}