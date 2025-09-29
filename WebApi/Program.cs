using Infrastructure.Configurations;
using Infrastructure.Extensions;
using Infrastructure.Hubs.Implementations;
using Infrastructure.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// DbContext
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), sqlOptions => sqlOptions.MigrationsAssembly("Infrastructure")));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// HttpContext
builder.Services.AddHttpContextAccessor();

// Signalr
builder.Services.AddSignalR(e =>
{
    e.EnableDetailedErrors = true;
    e.MaximumReceiveMessageSize = 102400000;
});

// AutoMapper
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

//// MediatR
//builder.Services.AddMediatR(configuration =>
//{
//    configuration.RegisterServicesFromAssembly(typeof(Program).Assembly);
//});

// CustomService
builder.Services.AddCustomService();

// Http
builder.Services.AddHttpClient();
builder.Services.AddHttpContextAccessor();

// Authorize and Swagger
builder.Services.AddSwaggerGen(c =>
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
string domain = builder.Configuration["AppSettings:JwtIssuer"];
JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

builder.Services.AddAuthentication(options =>
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
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["AppSettings:JwtKey"])),
        ClockSkew = TimeSpan.Zero // remove delay of token when expire
    };
});

// Cors
builder.Services.AddCors((options => {
    options.AddPolicy("NON.EXE", builder =>
    builder.SetIsOriginAllowed(c => true)
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials());
}));

//// Elasticsearch
//builder.Services.AddSingleton<ElasticsearchClient>(sp =>
//{
//    var settings = new ElasticsearchClientSettings(new Uri(""));
//    //.DefaultIndex("your-default-index")
//    //.Authentication(new BasicAuthentication("elastic", "your_password"));

//    return new ElasticsearchClient(settings);
//});

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
