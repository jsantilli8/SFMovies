using MediatR;
using Serilog;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using SFMovies.Application.Services;
using SFMovies.Api.Middleware;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Authorization;

namespace SFMovies.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Serilog
        builder.Host.UseSerilog((ctx, lc) => lc
            .WriteTo.Console()
            .ReadFrom.Configuration(ctx.Configuration));

        // Swagger
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.EnableAnnotations();
        });

        // Memory Cache
        builder.Services.AddMemoryCache();

        // MovieLocation cache service registration
        builder.Services.AddScoped<IMovieLocationService, MovieLocationCacheService>();

        // MediatR
        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(SFMovies.Application.Queries.GetAllMovieLocationsQuery).Assembly));

        // JWT Authentication
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
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                };
            });

        // Authorization with simplified roles
        builder.Services.AddAuthorization(options =>
        {
            // Basic role-based policies
            options.AddPolicy("Admin", policy => policy.RequireRole("Administrator"));
            options.AddPolicy("Viewer", policy => policy.RequireRole("Viewer"));
            
            // Permission-based policy combining both roles
            options.AddPolicy("LocationAccess", policy => 
                policy.RequireAssertion(context =>
                    context.User.IsInRole("Administrator") || 
                    context.User.IsInRole("Viewer")));
        });

        builder.Services.AddControllers();

        var app = builder.Build();

        // Configure the HTTP request pipeline
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseMiddleware<ExceptionHandlingMiddleware>();
        app.UseSerilogRequestLogging();
        
        // Authentication & Authorization middleware
        app.UseAuthentication();
        app.UseAuthorization();

        // CORS configuration for Vue.js frontend
        app.UseCors(builder => builder
            .WithOrigins("http://localhost:8080") // Vue.js default port
            .AllowAnyMethod()
            .AllowAnyHeader());

        app.MapControllers();

        app.Run();
    }
}
