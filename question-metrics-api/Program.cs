using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Filters;

namespace question_metrics_api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseSerilog((hostingContext, loggerConfiguration) => loggerConfiguration
                    .ReadFrom.Configuration(hostingContext.Configuration)
                    .Enrich.FromLogContext()
                    .Enrich.WithProperty("Stack", "QuestionMetrics")
                    .Enrich.WithProperty("Service", "Question Metrics API")
                    .WriteTo.Console()
                    .WriteTo.Logger(lc => 
                        lc.MinimumLevel.Error()
                        .WriteTo.Email(fromEmail: "no-reply@questionmetrics.com",
                                       toEmail: "rafael.miceli@hotmail.com",
                                       mailServer: "mailhog"))
                    .Filter.ByExcluding(c => c.MessageTemplate.Text.Contains("HealthChecks")))
                .Build();
    }
}
