using ApiRestaurante.Core.Application.Dto.Account;
using ApiRestaurante.Core.Application.Interfaces.Account;
using ApiRestaurante.Core.Domain.Settings;
using ApiRestaurante.Infraestructure.Identity.Context;
using ApiRestaurante.Infraestructure.Identity.Entities;
using ApiRestaurante.Infraestructure.Identity.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiRestaurante.Infraestructure.Identity
{
    public static class ServiceRegistrator
    {
        public static void AddIdentityInfraestructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<IdentityContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("Identityconexion"),
                    m => m.MigrationsAssembly(typeof(IdentityContext).Assembly.FullName));
            });


            
             services.AddIdentity<ApplicationUser, IdentityRole>()
             .AddEntityFrameworkStores<IdentityContext>().AddDefaultTokenProviders();

           /*  services.ConfigureApplicationCookie(options =>
             {
                 options.LogoutPath = "/Usuarios";
             });

             services.ConfigureApplicationCookie(options =>
             {
                 options.LoginPath = "/Usuarios";
                 options.AccessDeniedPath = "/Usuarios/AccessDenied";
             });*/

            services.Configure<JwtSettings>(configuration.GetSection("JWTSettings"));

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidIssuer = configuration[
                        "JWTSettings:Issuer"
                        ],
                    ValidAudience = configuration[
                        "JWTSettings:Audience"
                        ],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration[
                        "JWTSettings:Key"
                        ])),
                };
                options.Events = new JwtBearerEvents()
                {
                    OnAuthenticationFailed = c =>
                    {
                        c.NoResult();
                        c.Response.StatusCode = 500;
                        c.Response.ContentType = "text/plain";
                        return c.Response.WriteAsync(c.Exception.ToString());
                    },

                    OnChallenge = c =>
                    {
                        c.HandleResponse();
                        c.Response.StatusCode = 401;
                        c.Response.ContentType = "application/json";
                        var result = JsonConvert.SerializeObject(new JwtResponse { HasError = true, Error = "Your are not Authorized" });
                        return c.Response.WriteAsync(result);
                    },
                    OnForbidden = c =>
                    {

                        c.Response.StatusCode = 403;
                        c.Response.ContentType = "application/json";
                        var result = JsonConvert.SerializeObject(new JwtResponse { HasError = true, Error = "Your are not Authorized to acces this resourced" });
                        return c.Response.WriteAsync(result);
                    }
                };

            });

            #region "Services"
            services.AddTransient<IAccountServices, AccountServices>();
            #endregion

        }
    }
}
