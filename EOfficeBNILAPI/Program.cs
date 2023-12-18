using EOfficeBNILAPI.DataAccess;
using EOfficeBNILAPI.Jwt;
using EOfficeBNILAPI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

// Add services to the container.

//var sqlConnectionString = configuration.GetValue<string>("ConnectionStrings:Prod");
var sqlConnectionString = configuration.GetValue<string>("ConnectionStrings:Dev");
//var sqlConnectionString = Configuration.GetConnectionString("connectionData");

builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(sqlConnectionString));
builder.Services.AddScoped<IDataAccessProvider, DataAccessProvider>();
builder.Services.Configure<MailSettings>(configuration.GetSection("MailSettings"));
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters =
            new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,

                ValidIssuer = "BNILife.EOffice.Bearer",
                ValidAudience = "BNILife.EOffice.Bearer",
                IssuerSigningKey =
                JwtSecurityKey.Create("Eoffice1!-secret-key")
            };
    });
builder.Services.AddCors(
        options => options.AddPolicy("AllowCors", builder => { builder.AllowAnyOrigin().WithHeaders("Content-Type", "Authorization").WithMethods("GET", "POST", "PUT", "DELETE", "OPTIONS"); })
    );
builder.Services.AddControllers();
builder.Services.AddMvc().AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(s =>
{
    s.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Version = "V1",
        Title = "EOffice BNI Api",
        Description = "Api for all EOffice Process"
    });
    s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    s.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                {
                    new OpenApiSecurityScheme
                    {
                    Reference = new OpenApiReference
                        {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                        },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header,

                    },
                    new List<string>()
                    }
                });
    //var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    //var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    //s.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.UseCors("AllowCors");

app.MapControllers();

app.Run();
