using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MyFirstWebApi.Model;

namespace MyFirstWebApi
{
    //Windows
	//set "ConnectionStrings__default=Server=the-production-database-server; Database=DbContextFactorySample2; Trusted_Connection=True;"

    //Linux
	//export ConnectionStrings__default = "Server=the-production-database-server; Database=DbContextFactorySample2; Trusted_Connection=True;"

    public class Startup
    {
		public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
			var builder = new ConfigurationBuilder()
				.SetBasePath(env.ContentRootPath)
				.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
				.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
				.AddEnvironmentVariables();
			Configuration = builder.Build();
            //Configuration = configuration;
			var environment = Configuration["ApplicationSettings:Environment"];
			ConnectionString = Configuration.GetConnectionString("default");

        }

        public IConfiguration Configuration { get; }

		public static string ConnectionString {  
             get;  
             private set;  
        }  

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
			services.Configure<AppSettings>(Configuration.GetSection("ApplicationSettings"));
            //services.AddRouting();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            // The foolwoing 2 are the same.  #1 is a shorthand version
            //(1) app.UseMvcWithDefaultRoute();
            //(2) app.UseMvc(routes =>
            //{
            //    routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            //});

            // We are using attribute routing.  Recommended by MS to use attribute rotuing for Web Api
            //app.UseMvc();

            // this is conventional routing.  We should either use conventional routing or attribute routing.
            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "{controller}/{action}/{id?}");
            });
        }
    }
}
