using Application.Contexts.Abstractions;
using Application.Contexts.Implementations;
using Application.IReponsitories.Abstractions;
using Application.IReponsitories.Base;
using Application.Validations.Extend;
using FluentValidation;
using Infrastructure.Dapper.Abstractions;
using Infrastructure.Dapper.Implementations;
using Infrastructure.Features.MinIO.Queries;
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
using Minio.Implementations;
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
            #region Context
            // Context
            services.AddSingleton<IConnectionContext, ConnectionContext>();
            services.AddScoped<IUserContext, UserContext>();
            #endregion

            #region Repository
            // Repository
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IProductRepository, ProductRepository>();
            #endregion

            #region Service
            // Service      
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<IProductService, ProductService>();
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
            services.AddValidatorsFromAssemblyContaining<ProductValidation>();
            #endregion

            #region MediatR
            // MediatR
            services.AddMediatR(configuration =>
            {
                // Product
                configuration.RegisterServicesFromAssembly(typeof(DeleteMultipleProductQuery).Assembly);
                configuration.RegisterServicesFromAssembly(typeof(DeleteProductQuery).Assembly);
                configuration.RegisterServicesFromAssembly(typeof(GetProductByIdQuery).Assembly);
                configuration.RegisterServicesFromAssembly(typeof(GetProductByPageQuery).Assembly);
                configuration.RegisterServicesFromAssembly(typeof(SaveProductQuery).Assembly);
                // MinIO
                configuration.RegisterServicesFromAssembly(typeof(CopyObjectQuery).Assembly);
                configuration.RegisterServicesFromAssembly(typeof(CreateBucketQuery).Assembly);
                configuration.RegisterServicesFromAssembly(typeof(DeleteBucketQuery).Assembly);
                configuration.RegisterServicesFromAssembly(typeof(DeleteObjectQuery).Assembly);
                configuration.RegisterServicesFromAssembly(typeof(GetBucketByPageQuery).Assembly);
                configuration.RegisterServicesFromAssembly(typeof(GetObjectQuery).Assembly);
                configuration.RegisterServicesFromAssembly(typeof(GetObjectsByPageQuery).Assembly);
                configuration.RegisterServicesFromAssembly(typeof(GetPresignedObjectUrlQuery).Assembly);
                configuration.RegisterServicesFromAssembly(typeof(UploadObjectQuery).Assembly);
                configuration.RegisterServicesFromAssembly(typeof(UploadObjectWithPathQuery).Assembly);
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
