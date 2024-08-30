using MyBlogApp.Persistance;
using MyBlogApp.Infrastructure;
using Serilog;
using Serilog.Sinks.MSSqlServer;
using MyBlogApp.UI.Extensions;
using MyBlogApp.Infrastructure.Services.Storage;
using MyBlogApp.Infrastructure.Enums;
using FluentValidation.AspNetCore;
using MyBlogApp.Application.Validators;
using MyBlogApp.Application;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCache(builder.Environment.EnvironmentName, CachingType.InMemory);
builder.Services.AddStorageService<LocalStorageService>();
builder.Services.AddAplicationServices();
builder.Services.AddControllersWithViews().AddFluentValidation(fv =>
{
    fv.RegisterValidatorsFromAssemblyContaining<IValidatable>();

    //fv.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
});

//builder.Services.AddValidatorsFromAssemblyContaining<IValidatable>();
builder.Services.AddPersistanceServices(builder.Environment.EnvironmentName);

var dbConnectionString = Configuration.GetConnectionString(builder.Environment.EnvironmentName);

var loggerConf = new LoggerConfiguration()
    .WriteTo.File("logs/log.txt")
    .WriteTo.Console()
    .WriteTo.MSSqlServer(dbConnectionString, sinkOptions: new MSSqlServerSinkOptions
    {
        AutoCreateSqlTable = true,
        TableName = "logs"
    }).CreateLogger();

builder.Host.UseSerilog(loggerConf);

var app = builder.Build();

app.AddExceptionHandler(app.Services.GetRequiredService<ILogger<Program>>());

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
