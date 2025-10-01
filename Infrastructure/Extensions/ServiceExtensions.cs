using Application.Contexts.Abstractions;
using Application.Contexts.Implementations;
using Application.IReponsitories.Abstractions;
using Application.IReponsitories.Base;
using Application.Validations.Extend;
using FluentValidation;
using Infrastructure.Dapper.Abstractions;
using Infrastructure.Dapper.Implementations;
using Infrastructure.Features.Products.Queries;
using Infrastructure.Reponsitories.Base;
using Infrastructure.Reponsitories.Implementations;
using Infrastructure.Services.Abstractions;
using Infrastructure.Services.Implementations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Minio.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddCustomService(this IServiceCollection services)
        {
            #region Repository
            // Repository
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IProductRepository, ProductRepository>();
            #endregion

            #region Service
            // Service
            services.AddTransient<IUserContext, UserContext>();
            services.AddTransient<ILoginService, LoginService>();
            services.AddTransient<IProductService, ProductService>();
            #endregion

            #region Dapper
            // Dapper
            services.AddTransient<IDapperService, DapperService>();
            #endregion

            #region Minio
            // Minio
            services.AddTransient<IMinioService, IMinioService>();
            #endregion

            #region Validation
            // Validation
            services.AddValidatorsFromAssemblyContaining<ProductValidation>();
            #endregion

            #region MediatR
            // MediatR
            services.AddMediatR(configuration =>
            {
                configuration.RegisterServicesFromAssembly(typeof(DeleteMultipleProductQuery).Assembly);
                configuration.RegisterServicesFromAssembly(typeof(DeleteProductQuery).Assembly);
                configuration.RegisterServicesFromAssembly(typeof(GetProductByIdQuery).Assembly);
                configuration.RegisterServicesFromAssembly(typeof(GetProductByPageQuery).Assembly);
                configuration.RegisterServicesFromAssembly(typeof(SaveProductQuery).Assembly);
                //configuration.RegisterServicesFromAssembly(typeof(SearchProductElasticQuery).Assembly);
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
            #endregion
        }
        public static void AddDebugCustomService(this IServiceCollection services, IConfiguration configuration)
        {
        }

        public static void UseCustomService(this IApplicationBuilder app)
        {
        }
    }
}
