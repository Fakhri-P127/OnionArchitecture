
using Microsoft.EntityFrameworkCore;
using OnionArchitecture.Application.Interfaces.Repositories;

using OnionArchitecture.Application.ServiceRegistration;
using OnionArchitecture.Domain.Entities;
using OnionArchitecture.Persistance.Context;
using OnionArchitecture.Persistance.Repositories;
using OnionArchitecture.Persistance.ServiceRegistration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddPersistanceRegistrations(builder.Configuration);
builder.Services.AddApplicationRegistrations();


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
