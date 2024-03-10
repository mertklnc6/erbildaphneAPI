using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using Microsoft.AspNetCore.Authentication.Cookies;
using erbildaphneAPI.Service.Mapping;
using erbildaphneAPI.Service.Services;
using erbildaphneAPI.DataAccess.Data;
using erbildaphneAPI.DataAccess.UnitOfWorks;
using erbildaphneAPI.DataAccess.Repositories;
using erbildaphneAPI.Entity.Services;
using erbildaphneAPI.Entity.Repositories;
using erbildaphneAPI.Entity.UnitOfWorks;
using erbildaphneAPI.DataAccess.Identity.Models;
using Microsoft.AspNetCore.Http;

namespace erbildaphneAPI.Service.Extensions
{
    public static class DependencyExtensions
    {
        public static void AddExtensions(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
                {
                    options.LoginPath = new PathString("/account/login");
                    options.AccessDeniedPath = new PathString("/account/denied");
                })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = configuration["Jwt:Issuer"],
                        ValidAudience = configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
                    };
                });


            // Identity
            services.AddIdentity<AppUser, AppRole>(opt =>
            {
                // Password and lockout settings                       
                opt.User.RequireUniqueEmail = true;                
                
            })
            .AddEntityFrameworkStores<AppDbContext>();




            // Repositories and Services
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IArticleService, ArticleService>();
            services.AddScoped<IRumorService, RumorService>();
            services.AddScoped<IGNewsService, GNewsService>();
            services.AddScoped<IGNewsSourceService, GNewsSourceService>();
            services.AddScoped<IAuthorService, AuthorService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<ISecondArticleService, SecondArticleService>();            
            services.AddScoped<IMainNewsService, MainNewsService>();

            // AutoMapper
            services.AddAutoMapper(typeof(MappingProfile));
        }
    }
}
