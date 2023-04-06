using NLog.Web;
using SuperCarga.Api.Documentation;
using SuperCarga.Api.Exceptions;
using SuperCarga.Api.Settings;
using SuperCarga.Application.Registrations;
using SuperCarga.Domain.Registrations;
using SuperCarga.Persistence.Registrations;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var dbcs = configuration.GetConnectionString("SuperCargaConnectionString");

builder.Services.AddSettings(configuration);
builder.Services.AddCors(options =>
{
    options.AddPolicy("Open", builder => builder.AllowAnyOrigin().AllowAnyHeader());
});
builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
{
    options.SuppressMapClientErrors = true;
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();

builder.Services.AddApplication();
builder.Services.AddDomain();
builder.Services.AddPersistance(dbcs);

builder.Logging.ClearProviders();
builder.Host.UseNLog();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.UseCors("Open");
app.UseMiddleware<ExceptionHandlerMiddleware>();
app.UseWebSockets();
app.Run();
