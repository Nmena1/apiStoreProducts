﻿using apiStock.BLL.Services;
using apiStock.DAL.Context;
using apiStock.DAL.Repository;
using apiStock.DAL.Repository.Contract;
using apiStock.Utility;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.EntityFrameworkCore;
using System.Text;

using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using apiStock.BLL.Services.Contract;
using Microsoft.Extensions.Options;

namespace apiStock.IOC
{
    public static class Dependency
    {
        public static void InjectDependency(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<dbContext>(optionsAction =>
            {
                optionsAction.UseSqlServer(configuration.GetConnectionString("sqlConection"));
            });

            services.AddTransient(typeof(IGenerycRepository<>), typeof(GenerycRepository<>));
            
            //add automapper class
            services.AddAutoMapper(typeof(AutoMappeeProfile));

            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IMovementService, MovementService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<_encriptService>();
            services.AddScoped<_generateTKN>();
            services.AddScoped<_generalService>();

            //token
            // Configuración de JWT
            var jwtSettings = configuration.GetSection("Jwt");
            var key = Encoding.ASCII.GetBytes(jwtSettings["Key"]);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings["Issuer"],
                    ValidAudience = jwtSettings["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
            });


            //CORS
            // En Program.cs o Startup.cs
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAngularDev",
                    builder => builder
                        .WithOrigins("http://localhost:4200")
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
            });
        }



    }
}
