using Events_Web_Application.API.Controllers;
using Events_Web_Application.src.Application.Common.Mapping;
using Events_Web_Application.src.Application.Events.Commands.Command;
using Events_Web_Application.src.Application.Events.Commands.Handler;
using Events_Web_Application.src.Application.Events.Validators;
using Events_Web_Application.src.Domain.Entities;
using Events_Web_Application.src.Domain.interfaces;
using Events_Web_Application.src.Infastructure.Persistence;
using Events_Web_Application.src.Infastructure.Persistence.Repositories;
using Events_Web_Application.src.WebAPI.Controllers;
using Events_Web_Application.src.WebAPI.Extensions;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


// Auth

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy =>
        policy.RequireClaim(ClaimTypes.Role, "Admin"));

    options.AddPolicy("UserOnly", policy =>
        policy.RequireClaim(ClaimTypes.Role, "User"));

    options.AddPolicy("AtLeastUser", policy =>
        policy.RequireRole("User", "Admin"));

    options.AddPolicy("EmailVerified", policy =>
        policy.RequireAssertion(context =>
            context.User.HasClaim(c => c.Type == ClaimTypes.Email)));
});

builder.Services.AddHttpContextAccessor();
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));


//Config

builder.Services.AddControllers()
    .AddApplicationPart(typeof(UserController).Assembly)
    .AddFluentValidation(fv => {
        fv.RegisterValidatorsFromAssemblyContaining<CreateUserValidator>();
        fv.AutomaticValidationEnabled = true;
    })
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
        options.JsonSerializerOptions.IncludeFields = true;
    });

builder.Services.AddControllers()
    .AddApplicationPart(typeof(AuthController).Assembly)
    .AddFluentValidation(fv => {
        fv.RegisterValidatorsFromAssemblyContaining<CreateUserValidator>();
        fv.AutomaticValidationEnabled = true;
    })
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
        options.JsonSerializerOptions.IncludeFields = true;
    });

builder.Services.AddControllers()
    .AddApplicationPart(typeof(EventsController).Assembly)
    .AddFluentValidation(fv => {
        fv.RegisterValidatorsFromAssemblyContaining<CreateUserValidator>();
        fv.AutomaticValidationEnabled = true;
    })
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
        options.JsonSerializerOptions.IncludeFields = true;
    });

Console.WriteLine($"Base Path: {Directory.GetCurrentDirectory()}");
Console.WriteLine($"Loaded Connection String: {builder.Configuration.GetConnectionString("DefaultConnection")}");


//DB

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

Console.WriteLine($"Connection String: {connectionString}");

//AutoMapper
builder.Services.AddAutoMapper(typeof(EventMapper).Assembly);
builder.Services.AddAutoMapper(typeof(UserMapper).Assembly);
builder.Services.AddAutoMapper(typeof(ChangesMapper).Assembly);
builder.Services.AddAutoMapper(typeof(Program).Assembly);

//MediatR
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(CreateEventCommandHandler).Assembly);
});
builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IJwtService, JwtService>();

builder.Services.AddSwaggerGen();

//Fluent Validtion
builder.Services.AddValidatorsFromAssemblyContaining<CreateEventValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateUserValidator>();
builder.Services.AddControllers()
    .AddFluentValidation(fv => {
        fv.RegisterValidatorsFromAssemblyContaining<CreateUserValidator>();
        fv.AutomaticValidationEnabled = true; 
    });

builder.Services.AddControllers()
    .AddFluentValidation(fv => {
        fv.RegisterValidatorsFromAssemblyContaining<CreateEventValidator>();
        fv.AutomaticValidationEnabled = true;
    });

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
        options.JsonSerializerOptions.IncludeFields = true; 
    });

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

//swagger
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Description = "JWT Authorization header using the Bearer scheme."
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
            Array.Empty<string>()
        }
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

//Кэширование
builder.Services.AddMemoryCache();

var app = builder.Build();



app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.UseSwagger();
app.UseSwaggerUI();

app.Use(async (context, next) =>
{
    Console.WriteLine($"Token: {context.Request.Headers["Authorization"]}");
    await next();
});

app.Run();
    