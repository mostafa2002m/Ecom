using Application.Helper;
using Application.Interfaces;
using Application.Mapping;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Infrastructure.DependencyInjection
{
    public static class ServiceContainer
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
                                                                IConfiguration configuration)
        {

            //apply DbContext
            services.AddDbContext<AppDbContext>(o =>
            {
                o.UseSqlServer(configuration.GetConnectionString("DefaultConnection")

                    ??
                    throw new InvalidOperationException("connection string not found")
                    );
            });




            // Add identity & Jwt authentication



            // Authentication with Cookies
            services.AddAuthentication(AuthConstants.AuthScheme)
                .AddCookie(AuthConstants.AuthScheme, options =>
                {
                    options.Cookie.Name = "AuthScheme";
                    options.LoginPath = "/Login";
                    options.AccessDeniedPath = "/AccessDenied";
                    options.LogoutPath = "/Logout";
                    options.Cookie.HttpOnly = true;
                    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                    options.Cookie.SameSite = SameSiteMode.Strict;

                    options.ExpireTimeSpan = TimeSpan.FromDays(1);
                    options.SlidingExpiration = true;

                });

            // Jwt
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey
                    (Encoding.UTF8.GetBytes(configuration["Jwt:securityKey"]!))
                };
            });


            services.AddAuthorizationCore();


            services.AddAutoMapper(cfg => { }, typeof(MapperInitializer).Assembly);

            services.AddCors(option =>
            {
                option.AddPolicy(name: "Cors",
                    builder =>
                    {
                        builder.WithOrigins(configuration["BackendUrl"] ?? "",
                            configuration["FrontendUrl"] ?? "")
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .AllowCredentials();
                    });
            });

           
            services.AddControllers().AddNewtonsoftJson(x =>
                x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            //services.AddScoped<IUserRepo, UserRepo>();

            //apply UnitOf Work
            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
            services.AddSingleton<IImageManagementService, ImageManagementService>();
            services.AddSingleton<IFileProvider>(new PhysicalFileProvider(
                Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")));

            return services;
        }
    }
}
