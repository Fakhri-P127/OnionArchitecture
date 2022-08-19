
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using OnionArchitecture.Application.Filters;
using OnionArchitecture.Application.Interfaces.Repositories;

using OnionArchitecture.Application.ServiceRegistration;
using OnionArchitecture.Domain.Entities;
using OnionArchitecture.Persistance.Context;
using OnionArchitecture.Persistance.Repositories;
using OnionArchitecture.Persistance.ServiceRegistration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(/*opt =>opt.Filters.Add<VersionDiscontinueResourceFilter>()*/)
    .AddNewtonsoftJson(opt=> opt
.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddPersistanceRegistrations(builder.Configuration);
builder.Services.AddApplicationRegistrations();
builder.Services.AddApiVersioning(opt =>
{
    opt.ReportApiVersions = true;
    opt.AssumeDefaultVersionWhenUnspecified = true;
    opt.DefaultApiVersion = new ApiVersion(1, 0);
    //opt.ApiVersionReader = new HeaderApiVersionReader("X-API-VERSION");
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
//app.UseApiVersioning();
app.MapControllers();

app.Run();
