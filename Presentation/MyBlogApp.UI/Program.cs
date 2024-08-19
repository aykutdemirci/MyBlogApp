using MyBlogApp.Persistance;
using MyBlogApp.Infrastructure;
using Serilog;
using Serilog.Sinks.MSSqlServer;
using MyBlogApp.UI.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCache(MyBlogApp.Infrastructure.Enums.CachingType.InMemory);
builder.Services.AddControllersWithViews();

builder.Services.AddPersistanceServices();

var loggerConf = new LoggerConfiguration()
    .WriteTo.File("logs/log.txt")
    .WriteTo.Console()
    .WriteTo.MSSqlServer(Configuration.DbConnectionString, sinkOptions: new MSSqlServerSinkOptions
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
