using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;

namespace apigateway
{
    public class Startup
    {
        private readonly IConfiguration c_configuration;


        public Startup(IConfiguration configuration)
        {
            this.c_configuration = configuration;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(
            IServiceCollection services)
        {
            var _appSettings = this.c_configuration.Get<AppSettings>(options => options.BindNonPublicProperties = true);

            services
                .AddControllers(options =>
                {
                    options.Filters.Add(new Filters.ModelStateValidationFilter());
                    options.Filters.Add(new Filters.ExceptionFilter());
                })
                .AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);

            services.AddLogging(builder =>
                builder
                    .AddDebug()
                    .AddConsole()
                    .AddConfiguration(this.c_configuration.GetSection("Logging"))
                    .SetMinimumLevel(LogLevel.Information)
            );

            services
                .AddSingleton<External.Reporting.IClient, apigateway.External.Reporting.Client>()
                .AddSingleton<External.Rates.IClient, apigateway.External.Rates.Client>()
                .AddSingleton<External.BINs.IClient, apigateway.External.BINs.Client>()
                .AddSingleton(_appSettings);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
