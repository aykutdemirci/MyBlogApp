using MyBlogApp.Infrastructure.Enums;
using MyBlogApp.Infrastructure;
using MyBlogApp.Infrastructure.Services.Storage;
using MyBlogApp.Application;
using MyBlogApp.Persistance;
using Serilog;
using Serilog.Sinks.MSSqlServer;
using MyBlogApp.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var env = builder.Environment.EnvironmentName;
builder.Configuration.AddJsonFile($"appsettings.{env}.json", optional: false).AddEnvironmentVariables();

builder.Services.AddControllers();
builder.Services.AddCache(env, CachingType.InMemory);
builder.Services.AddStorageService<AzureStorageService>();
builder.Services.AddAplicationServices();
builder.Services.AddPersistanceServices(env);

var dbConnectionString = Configuration.GetConnectionString(env);

var loggerConf = new LoggerConfiguration()
    .WriteTo.File("logs/log.txt")
    .WriteTo.Console()
    .WriteTo.MSSqlServer(dbConnectionString, sinkOptions: new MSSqlServerSinkOptions
    {
        AutoCreateSqlTable = true,
        TableName = "logs"
    }).CreateLogger();

builder.Host.UseSerilog(loggerConf);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.AddExceptionHandler(app.Services.GetRequiredService<ILogger<Program>>());

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
