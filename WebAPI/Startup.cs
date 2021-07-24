using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.AzureAD.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.ADDAzure;
using WebAPI.Common.Configuration;
using WebAPI.InterfaceADDAzure;
using WebAPI.Models;
using WebAPI.Services;
using UserReadADDAzure = WebAPI.Services.UserReadADDAzure;

namespace WebAPI
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
            //services.AddAuthentication(AzureADDefaults.AuthenticationScheme)
            //   .AddAzureAD(options => Configuration.Bind("AzureAd", options));
            //services.AddControllers(options =>
            //{
            //    var policy = new AuthorizationPolicyBuilder()
            //        .RequireAuthenticatedUser()
            //        .Build();
            //    options.Filters.Add(new AuthorizeFilter(policy));
            //});
            services.AddControllers();
            services.AddDbContext<DatabaseContext>(options =>
             options.UseSqlServer(
                 Configuration.GetConnectionString("DefaultConnection")));
            services.Configure<GetConnectADDAzure>(Configuration.GetSection(GetConnectADDAzure.ConnectAzureAD));
            services.AddSingleton<IUserADDAzure, GraphApiClientDirect>();
            services.AddSingleton<IApiPermissions, UserReadADDAzure>();
            var contact = new OpenApiContact()
            {
                Name = "Hien Truong",
                Email = "user@example.com",
                Url = new Uri("http://www.example.com")
            };
            var license = new OpenApiLicense()
            {
                Name = "Duc Hien",
                Url = new Uri("http://www.example.com")
            };
            var info = new OpenApiInfo()
            {
                Version = "v1",
                Title = "Swagger Demo API",
                Description = "Swagger Demo API Description",
                TermsOfService = new Uri("http://www.example.com"),
                Contact = contact,
                License = license
            };
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", info);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json",
                "Swagger Demo API v1");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
