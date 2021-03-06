using Hsc.ApiFramework.Authentication.DependencyInjection;
using Hsc.ApiFramework.Core.Database;
using Hsc.ApiFramework.Database.SqlServer.DependencyInjection;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// Configure swagger to add Authentication button
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Hsc.SampleWebApi",
        Version = "v1"
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "JWT Bearer token",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
     {
       new OpenApiSecurityScheme
       {
         Reference = new OpenApiReference
         {
           Type = ReferenceType.SecurityScheme,
           Id = "Bearer"
         }
        },
        new string[] { }
      }
    });
});

//Add database connection
//Reliant on HSC_DATABASE_CONNECTION environment variable for it's connection string
//works with any IdentityDbContext<T> 
//Overloads are available to specify IdentityUser and IdentityRole types.
builder.Services.AddHscDatabase<HscDatabaseContext>(migrationsAssembly: "Hsc.SampleWebApi");

//Add authentication endpoints
    //Reliant on HSC_JWT_AUDIENCE for JWT token Audience - Usually set to the URL of the issueing server (http://localhost:{port} when running locally)
    //Reliant on HSC_JWT_ISSUER for JWT token Issuer - Usually set to the URL of the issueing server (http://localhost:{port} when running locally)
    //Reliant on HSC_JWT_SECRET secret used for JWT Token generation
builder.Services.AddHscAuthentication<HscDatabaseContext>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//use HSC Authentication
    //Has two parameters:
        //useAuthentication - only set this to false if you've already used app.UseAuthentication() in another method
        //useAuthorization - only set this to false if you've already used app.useAuthorization() in another method
app.UseHscAuthentication();

app.MapControllers();

app.Run();
