using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace WebAPISvc
{
    public class Startup
    {
        
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            

            //dodavanje Autorizacije
            services.AddMvcCore()
            .AddAuthorization()
            .AddJsonFormatters();

            //dodavanje autentifikacije preko JwtBearer tokena koji se generira sa Identity server 
            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.Authority = "http://localhost:50000";
                    options.RequireHttpsMetadata = false;
                    
                    options.Audience = "adminScope";
                });


            //dodavanje DI za MVC, Autorizaciju
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();

            app.UseMvc();


            app.Run(async (context) =>
            {
                var auth = "authenticated";
                if (!context.User.Identity.IsAuthenticated)
                    auth = "unauthenticated";
                await context.Response.WriteAsync($"The user is {auth}.");
            });
        }

      
    }
}
