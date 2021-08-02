using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using WebAPI.BuisinessLogic;
using WebAPI.Common.Configuration;
using WebAPI.Interfaces;
using WebAPI.Middleware;
using WebAPI.Models;
using WebAPI.Services;
using WebAPI.Swagger;

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
          
            services.AddControllers();
            services.AddDbContext<DatabaseContext>(options =>
             options.UseSqlServer(
                 Configuration.GetConnectionString("DefaultConnection")));
            services.Configure<GetConnectADDAzure>(Configuration.GetSection(GetConnectADDAzure.ConnectAzureAD));
            services.AddSingleton<IUserADLogic,UserAzureADLogic>();
            services.AddSingleton<IGraphApiServices,GraphApiServices>();
            services.AddSingleton<AuthTokenMiddleware>();
            services.Configure<GetAuthToken>(Configuration.GetSection(GetAuthToken.AuthToken));
            services.AddHttpContextAccessor();
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
                c.OperationFilter<MyHeaderFilter>();

                // Swagger 2.+ support
                //                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                //                {
                //                    Description =
                //         "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                //                    Name = "Authorization",
                //                    In = ParameterLocation.Header,
                //                    Type = SecuritySchemeType.ApiKey,
                //                    Scheme = "Bearer"
                //                });

                //                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                //{
                //    {
                //        new OpenApiSecurityScheme
                //        {
                //            Reference = new OpenApiReference
                //            {
                //                Type = ReferenceType.SecurityScheme,
                //                Id = "Bearer"
                //            },
                //            Scheme = "oauth2",
                //            Name = "Bearer",
                //            In = ParameterLocation.Header,

                //        },
                //        new List<string>()
                //    }
                //});
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
                c.RoutePrefix = string.Empty;
            });   
            //app.Map("/swagger", (app1) =>
            // {
            //     app1.Run(async (context) =>
            //     {
            //         await context.Response.WriteAsync("hello");
            //     });
            // });
            //app.UseMiddleware<FirstMiddleware>();    
            app.UseMiddleware<AuthTokenMiddleware>();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

    }
}
