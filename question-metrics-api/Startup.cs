using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using question_metrics_data;
using question_metrics_domain.Interfaces;
using Serilog;
using Swashbuckle.AspNetCore.Swagger;

namespace question_metrics_api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Exams Metrics", Version = "v1" });
            });

            services.AddTransient<IExamRepo, ExamRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Exams Metrics V0.1");
            });

            app.UseExceptionHandler(eh => {
                eh.Run(
                    async context => {
                        context.Response.StatusCode = 500;
                        context.Response.ContentType = "application/json";

                        var errorCtx = context.Features.Get<IExceptionHandlerFeature>();
                        if (errorCtx != null)
                        {
                            var ex = errorCtx.Error;
                            Log.Error(ex, "Erro desconhecido");

                            var errorId = Activity.Current?.Id ?? context.TraceIdentifier;
                            var jsonResponse = JsonConvert.SerializeObject(new 
                            {
                                ErrorId = errorId,
                                Message = "Some kind of error happened in the API."
                            });
                            await context.Response.WriteAsync(jsonResponse, Encoding.UTF8);
                        }
                    }
                );
            });

            app.UseMvc();
        }
    }
}
