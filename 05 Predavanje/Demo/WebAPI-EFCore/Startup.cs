using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebAPI_EFCore.Models;
using WebAPI_EFCore.Repositories;

namespace WebAPI_EFCore
{
    public class Startup
    {
        //property konfiguracija koji sadrži informacije iz config datoteke
        public IConfiguration Configuration { get; set; }

        //preklopljeni konstruktor koji prihvata konfiguraciju objekta generiran iz web.config datoteka
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //dodavanje podrske za EF core i SQL provajdera sa stringm konekcije
            services.AddDbContext<EmployeeContext>(o => o.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            //dodavanje podrske za EmployeeRepository
            services.AddScoped<IDataRepository<Employee>, EmployeeManager>();

            //dodavanje podrske za MVC
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //ukljucivanje MVC podrske rutiranja u Web APIu
            app.UseMvc();

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Web API Core!");
            });
        }
    }
}
