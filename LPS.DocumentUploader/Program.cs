using LPS.DocumentUploader.Application.ConfigProfiles;
using LPS.DocumentUploader.Application.Services.Documents;
using LPS.DocumentUploader.Application.Services.Logins;
using LPS.DocumentUploader.Application.Services.Notifications;
using LPS.DocumentUploader.Application.Services.Users;
using LPS.DocumentUploader.ConfigProfiles;
using LPS.DocumentUploader.Database.Databases;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DBConnection");
builder.Services.AddDbContext<LPSDBContext>(option => option.UseSqlServer(connectionString));

//Add JWT Token Service
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

// AutoMapper
var config = new AutoMapper.MapperConfiguration(cfg =>
{
    cfg.AddProfile(new ConfigurationProfiles());
    cfg.AddProfile(new ConfigProfile());
});
var mapper = config.CreateMapper();

builder.Services.AddSingleton(mapper);
builder.Services.AddTransient<IDocumentAppService, DocumentAppService>();
builder.Services.AddTransient<ILoginAppService, LoginAppService>();
builder.Services.AddTransient<IUserAppService, UserAppService>();
builder.Services.AddSingleton<IEmailAppService>(new EmailAppService("smtp-server", 587, "smtp-username", "smtp-password"));

builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 2147483648; 
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(cfg =>
{
    cfg.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "JWTToken_Auth_API",
        Version = "v1"
    });
    cfg.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer Scheme. \r\n\r\n Enter 'Bearer' [Space] and then your token to the text input \r\n\r\n" +
                "Example: \"Bearer ey1blablabla\""
    });
    cfg.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            new string[]{ }
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
