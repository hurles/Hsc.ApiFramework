using Hsc.ApiFramework.Authentication.DependencyInjection;
using Hsc.ApiFramework.Core.Database;
using Hsc.ApiFramework.Database.SqlServer.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHscDatabase<HscDatabaseContext>();
builder.Services.ConfigureHscAuthentication<HscDatabaseContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//use HSC Authentication instead of 
app.UseHscAuthentication();

app.MapControllers();

app.Run();
