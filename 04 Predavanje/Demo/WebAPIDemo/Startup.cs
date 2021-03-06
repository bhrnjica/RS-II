﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace WebAPIDemo
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //dodavanje podrske za MVC
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //Upravljanje greškama u toku razvoja
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else//Upravljanje greškama u produkciji
            {
                app.UseExceptionHandler("/error");
            }

            //ukljucivanje MVC podrske rutiranja u Web APIu
            app.UseMvc();

            //
            app.Run(async (context) =>
            {
                //throw new System.Exception("Forced ASP.NET Core to throw an Exception!");
                await context.Response.WriteAsync("No MVC Response!");
            });
        }
    }
}
