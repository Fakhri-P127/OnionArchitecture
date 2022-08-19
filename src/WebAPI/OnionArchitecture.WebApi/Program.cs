
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
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
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "My WebApi v1", Version = "version #1" });
    opt.SwaggerDoc("v2", new OpenApiInfo { Title = "My WebApi v2", Version = "version #2" });
});
builder.Services.AddPersistanceRegistrations(builder.Configuration);
builder.Services.AddApplicationRegistrations();
builder.Services.AddApiVersioning(opt =>
{
    opt.ReportApiVersions = true;
    opt.AssumeDefaultVersionWhenUnspecified = true;
    opt.DefaultApiVersion = new ApiVersion(1, 0);
    opt.ApiVersionReader = new HeaderApiVersionReader("X-API-VERSION");
});
builder.Services.AddVersionedApiExplorer(opt=>opt.GroupNameFormat="'v'VVV");
var app = builder.Build();

app.UseHttpLogging();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(opt =>
    {
        opt.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApi v1");
        opt.SwaggerEndpoint("/swagger/v2/swagger.json", "WebApi v2");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();
//app.UseApiVersioning();
app.MapControllers();

app.Run();
