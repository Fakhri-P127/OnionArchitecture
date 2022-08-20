
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.OpenApi.Models;
using OnionArchitecture.Application.ServiceRegistration;
using OnionArchitecture.Persistance.ServiceRegistration;
using OnionArchitecture.WebApi.BackgroundServices;
using OnionArchitecture.WebApi.Extensions;
using Serilog;
using Serilog.Events;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .MinimumLevel.Override("Microsoft",LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .WriteTo.File(@"C:\Users\efend\Desktop\sa.txt")
    .CreateLogger();


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
builder.Services.AddHealthChecks();
builder.Services.AddHostedService<DateTimeLogWriter>();
builder.Services.AddHttpClient("ghibliApi", config =>
{
    config.BaseAddress = new Uri("https://ghibliapi.herokuapp.com");
    //config.DefaultRequestHeaders.Add("Authorization", "Bearer eqidoqsdjasjklfhq1i1e");
});
builder.Host.UseSerilog();
//builder.Host.UseWindowsService();

var app = builder.Build();

//app.UseHttpLogging();
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
app.UseCustomHealthCheck();
app.UseResponseCaching();
app.UseAuthorization();
//app.UseApiVersioning();
app.MapControllers();

try
{
    Log.Information("Starting up service");
    app.Run();
    return;
}
catch (Exception ex)
{
    Log.Fatal(ex.Message, "There was a problem");
    return;
}
finally
{
    Log.CloseAndFlush();
}
