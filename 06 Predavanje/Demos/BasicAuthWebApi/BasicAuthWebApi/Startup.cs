using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using BasicAuthWebApi.Authentication;
using Microsoft.AspNetCore.Http;

namespace BasicAuthWebApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            // Configure basic authentication 
            services.AddAuthentication("BasicAuthentication")
                .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);

            // Register DI for user service
            services.AddScoped<IUserService, UserService>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
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

            //
            app.Run(async (context) =>
            {
                //throw new System.Exception("Forced ASP.NET Core to throw an Exception!");
                if(!context.Authentication.HttpContext.User.Identity.IsAuthenticated)
                    await context.Response.WriteAsync("No User is logged in!");
                else
                    await context.Response.WriteAsync("No MVC Response!");
            });
        }
    }
}
