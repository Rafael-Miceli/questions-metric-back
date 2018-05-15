using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using question_metrics_domain;
using question_metrics_domain.Interfaces;
using Serilog;

namespace question_metrics_api.Controllers
{
    [Route("api/[controller]")]
    public class TestLogEmailController : Controller
    {

        // GET api/values
        [HttpGet]
        [Route("Test")]
        public async Task<IActionResult> Test()
        {
            Log.Error("Esta mesnagem de erro deve chegar no e-mail");
            Log.Warning("Esta mensagem de warning não deve cgear no email");

            return Ok();
        }

    }
}
