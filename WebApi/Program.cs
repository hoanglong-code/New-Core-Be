using Elastic.Clients.Elasticsearch;
using Elastic.Transport;
using Infrastructure.Configurations;
using Infrastructure.Extensions;
using Infrastructure.Filters;
using Infrastructure.Hubs.Implementations;
using Infrastructure.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Minio;
using StackExchange.Redis;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Runtime.Intrinsics.X86;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// DbContext
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), sqlOptions => sqlOptions.MigrationsAssembly("Infrastructure")));

builder.Services.AddControllers();
builder.Services.AddControllersWithViews(options =>
                options.Filters.Add(typeof(GlobalExceptionFilter))).AddNewtonsoftJson(
                (cfg =>
                {
                    cfg.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                }));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

#region Signalr
// Signalr
builder.Services.AddSignalR(e =>
{
    e.EnableDetailedErrors = true;
    e.MaximumReceiveMessageSize = 102400000;
});
#endregion

#region AutoMapper
// AutoMapper
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
#endregion

#region MediatR
// MediatR
builder.Services.AddMediatR(configuration =>
{
    configuration.RegisterServicesFromAssembly(typeof(Program).Assembly);
});
#endregion

#region Minio
// Minio
builder.Services.AddSingleton<IMinioClient>(sp =>
{
    return new MinioClient()
        .WithEndpoint(builder.Configuration["Minio:Endpoint"])
        .WithCredentials(
            builder.Configuration["Minio:AccessKey"],
            builder.Configuration["Minio:SecretKey"])
        .WithSSL(bool.Parse(builder.Configuration["Minio:Ssl"] ?? "false"))
        .Build(); // return IMinioClient
});
#endregion

#region Redis
// Redis
builder.Services.AddSingleton<IConnectionMultiplexer>(
    ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("Redis"))
);
#endregion

#region CustomService
// CustomService
builder.Services.AddCustomService();
builder.Services.AddAuthorizeService(builder.Configuration);
#endregion

#region Http
// Http
builder.Services.AddHttpClient();
builder.Services.AddHttpContextAccessor();
#endregion

#region Cors
// Cors
builder.Services.AddCors((options => {
    options.AddPolicy("NON.EXE", builder =>
    builder.SetIsOriginAllowed(c => true)
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials());
}));
#endregion

#region Elasticsearch
// Elasticsearch
builder.Services.AddSingleton<ElasticsearchClient>(sp =>
{
    var settings = new ElasticsearchClientSettings(new Uri(builder.Configuration["ElasticSearch:Url"]))
    //.DefaultIndex("your-default-index")
    .Authentication(new BasicAuthentication(builder.Configuration["ElasticSearch:Username"], builder.Configuration["ElasticSearch:Password"]))
    .EnableDebugMode()
    .PrettyJson()
    .ServerCertificateValidationCallback((sender, cert, chain, errors) => true);

    return new ElasticsearchClient(settings);
});
#endregion

#region Other
// Other
builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true; // Bật nén cả với HTTPS
    options.Providers.Add<GzipCompressionProvider>(); // Sử dụng Gzip
});

builder.Services.Configure<GzipCompressionProviderOptions>(options =>
{
    options.Level = System.IO.Compression.CompressionLevel.Fastest; // Cấu hình mức nén
});

builder.Services.Configure<IISServerOptions>(options =>
{
    options.MaxRequestBodySize = long.MaxValue;
});

builder.Services.Configure<FormOptions>(options =>
{
    options.ValueLengthLimit = int.MaxValue;
    options.MultipartBodyLengthLimit = long.MaxValue; // <-- ! long.MaxValue
    options.MultipartBoundaryLengthLimit = int.MaxValue;
    options.MultipartHeadersCountLimit = int.MaxValue;
    options.MultipartHeadersLengthLimit = int.MaxValue;
});
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHsts();
app.UseMiddleware<UserContextMiddleware>();
app.UseCors("NON.EXE");

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCookiePolicy();
app.UseRouting();


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapHub<HubClient>("/notify");

app.Run();
