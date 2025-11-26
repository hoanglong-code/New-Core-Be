using Application.Contexts.Abstractions;
using Application.Contexts.Implementations;
using Application.IReponsitories.Abstractions;
using Application.IReponsitories.Base;
using Application.Validations.Extend;
using FluentValidation;
using Infrastructure.CustomAuthorize;
using Infrastructure.Dapper.Abstractions;
using Infrastructure.Dapper.Implementations;
using Infrastructure.Features.Brands.Queries;
using Infrastructure.Features.Functions.Queries;
using Infrastructure.Features.MinIO.Queries;
using Infrastructure.Features.Products.Queries;
using Infrastructure.Features.Roles.Queries;
using Infrastructure.Reponsitories.Base;
using Infrastructure.Reponsitories.Implementations;
using Infrastructure.Services.Abstractions;
using Infrastructure.Services.Implementations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Minio.Abstractions;
using Minio.Implementations;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddCustomService(this IServiceCollection services)
        {
            #region Context
            // Context
            services.AddSingleton<IConnectionContext, ConnectionContext>();
            services.AddScoped<IUserContext, UserContext>();
            #endregion

            #region Repository
            // Repository
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IBrandRepository, BrandRepository>();
            services.AddScoped<IFunctionRepository, FunctionRepository>();
            services.AddScoped<IFunctionRoleRepository, FunctionRoleRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserRoleRepository, UserRoleRepository>();
            #endregion

            #region Service
            // Service      
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<IBrandService, BrandService>();
            services.AddScoped<IFunctionService, FunctionService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IMenuService, MenuService>();
            #endregion

            #region Dapper
            // Dapper
            services.AddTransient<IDapperService, DapperService>();
            #endregion

            #region Minio
            // Minio
            services.AddSingleton<IMinioService, MinioService>();
            #endregion

            #region Validation
            // Validation
            services.AddValidatorsFromAssemblyContaining<BrandValidation>();
            services.AddValidatorsFromAssemblyContaining<FunctionValidation>();
            services.AddValidatorsFromAssemblyContaining<ProductValidation>();
            services.AddValidatorsFromAssemblyContaining<RoleValidation>();
            services.AddValidatorsFromAssemblyContaining<UserValidation>();
            #endregion

            #region MediatR
            // MediatR
            services.AddMediatR(configuration =>
            {
                configuration.RegisterServicesFromAssembly(typeof(SaveBrandQuery).Assembly);
                configuration.RegisterServicesFromAssembly(typeof(SaveFunctionQuery).Assembly);
                configuration.RegisterServicesFromAssembly(typeof(SaveProductQuery).Assembly);
                configuration.RegisterServicesFromAssembly(typeof(SaveRoleQuery).Assembly);
                configuration.RegisterServicesFromAssembly(typeof(CopyObjectQuery).Assembly);
            });
            #endregion

            #region AutoMapper
            // AutoMapper
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            //var coreMappingAssembly = typeof(EntityMapping).Assembly;
            //services.AddAutoMapper(coreMappingAssembly);

            #endregion

            #region Other
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IAuthorizationHandler, PrivilegeHandler>();
            services.AddSingleton<IAuthorizationPolicyProvider, CustomPolicyProvider>();
            #endregion
        }
        public static void AddAuthorizeService(this IServiceCollection services, IConfiguration configuration)
        {
            // Authorize and Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CleanArchitectureNET", Version = "v1", Description = "APis are built for CleanArchitectureNET system by Long" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] {}
                    }
                });
            });
            string domain = configuration["AppSettings:JwtIssuer"];
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;
                cfg.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = domain,
                    ValidAudience = domain,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["AppSettings:JwtKey"])),
                    ClockSkew = TimeSpan.Zero // remove delay of token when expire
                };
            });
        }
        public static void UseCustomService(this IApplicationBuilder app)
        {
        }
    }
}
