﻿namespace StudentInfoSys.Application
{
    using System.Collections.Generic;
    using System.Text;
    using AutoMapper;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.IdentityModel.Tokens;
    using StudentInfoSys.Application.Helpers;
    using StudentInfoSys.Application.Models;
    using StudentInfoSys.Domain.Interfaces.Logging;
    using StudentInfoSys.Domain.Interfaces.Repositories;
    using StudentInfoSys.Domain.Interfaces.Services;
    using StudentInfoSys.Infrastructure;
    using StudentInfoSys.Infrastructure.Logging;
    using StudentInfoSys.Infrastructure.Repositories;
    using StudentInfoSys.Service;
    using Swashbuckle.AspNetCore.Swagger;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            #region DbContext
            services.AddDbContext<StudentInfoSysDbContext>(opt => opt.UseSqlServer(this.Configuration.GetConnectionString("StudentInfoSysConnection"),
                b => b.MigrationsAssembly("StudentInfoSys.Infrastructure")));
            #endregion

            #region  Automapper
            services.AddAutoMapper(typeof(Startup).Assembly);
            #endregion

            #region Authentication
            var key = Encoding.ASCII.GetBytes(this.Configuration.GetSection("Key").Get<Key>().JWT);
            services.AddSingleton(this.Configuration.GetSection("Key").Get<Key>());

            services.AddAuthentication(authOpts =>
            {
                authOpts.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOpts.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(jwtOpts =>
            {
                jwtOpts.RequireHttpsMetadata = false;
                jwtOpts.SaveToken = true;
                jwtOpts.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            })
            .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);
            #endregion

            #region  Services
            services.AddScoped(typeof(IBaseLogger<>), typeof(Logger<>));
            services.AddScoped(typeof(IAsyncRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<IEnrollmentService, EnrollmentService>();
            #endregion

            #region Swagger
            services.AddSwaggerGen(swag =>
            {
                swag.SwaggerDoc("v1", new Info { Title = "StudentInfoSys API" });
                swag.AddSecurityDefinition("Basic", new BasicAuthScheme());
                swag.AddSecurityDefinition("Bearer", new ApiKeyScheme()
                {
                    Name = "Authorization",
                    In = "Header",
                    Description = "Value: bearer 'jwt-token' Note: Logout of basic auth first"
                });
                swag.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>
                {
                    { "Basic", new string[] { } },
                    { "Bearer", new string[] { } }
                });
            });
            #endregion

            #region MVC
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            #endregion
        }


        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            #region Exception Page
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            #endregion

            #region HTTPS
            app.UseHttpsRedirection();
            app.UseCors(opt => opt
                .AllowAnyHeader()
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowCredentials());
            #endregion

            #region Authentication
            app.UseAuthentication();
            #endregion

            #region Swagger
            app.UseSwagger();
            app.UseSwaggerUI(swag =>
            {
                swag.SwaggerEndpoint("/swagger/v1/swagger.json", "StudentInfoSys API v1");
                swag.RoutePrefix = string.Empty;
            });
            #endregion

            #region MVC
            app.UseMvc();
            #endregion
        }
    }
}
