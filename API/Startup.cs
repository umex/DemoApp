using System.Text;
using API.Data;
using API.Extensions;
using API.Interfaces;
using API.Middleware;
using API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
namespace API
{
    public class Startup
    {
        private readonly IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config;
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // DIs
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddApplicationServices(_config);
            services.AddControllers();
            services.AddCors();
            services.AddIdentityServices(_config);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            /*
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            */
            app.UseMiddleware<ExceptionMiddleware>();

            app.UseHttpsRedirection();

            app.UseRouting();

            //order is important!
            app.UseCors(policy => policy.AllowAnyHeader().AllowAnyHeader().WithOrigins("https://localhost:4200"));

            app.UseAuthentication();

            app.UseAuthorization();
            
            if(!Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT").Equals("Development")){
                app.UseDefaultFiles();
                app.UseStaticFiles();
            }

            //da uporablja fajle iz wwwroot
            app.UseDefaultFiles();
            app.UseStaticFiles();
            


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                //da ve kako odreagirat ce refreshnes na endpointu
                endpoints.MapFallbackToController("Index", "Fallback");
            });
        }
    }
}
