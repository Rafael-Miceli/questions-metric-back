﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Serilog.Filters;
using Serilog.Sinks.Email;

namespace question_metrics_api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuild(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuild(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseSerilog((hostingContext, loggerConfiguration) => loggerConfiguration
                    .ReadFrom.Configuration(hostingContext.Configuration)
                    .Enrich.FromLogContext()
                    .Enrich.WithProperty("Stack", "QuestionMetrics")
                    .Enrich.WithProperty("Service", "Question Metrics API")
                    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                    .WriteTo.Console()
                    .WriteTo.Logger(lc => 
                        lc.MinimumLevel.Error()
                        .Enrich.WithProperty("ERROR", "Inside Error!!!")
                        .WriteTo.Console()
                        .WriteTo.Email(new EmailConnectionInfo(){
                            FromEmail = "no-reply@questionmetrics.com",
                            ToEmail = "rafael.miceli@hotmail.com",
                            MailServer = "localhost",
                            Port = 1025
                        }))
                    .Filter.ByExcluding(c => c.Properties.Any(p => p.Value.ToString().Contains("swagger")))
                );
    }
}
